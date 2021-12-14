﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.Extensions.Hashing;
using Gex.Extensions.Response;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gex.Services;

using static GexResponse;

public class UsuarioService : IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<GexResult<UsuarioResponse>> CreateUsuarioAsync(RegistroRequest request)
    {
        try
        {
            if(await _usuarioRepository.ExistsUsuarioByEmailAsync(request.Email))
                return KeyError<UsuarioResponse>(nameof(request.Email), $"Esa dirección de correo electrónico {request.Email} ya existe.");

            var usuario = _mapper.Map<Usuario>(request);
            HashedPassword hashedPassword = request.Password.Hash();
            usuario.Password = hashedPassword.Password;
            usuario.Salt = hashedPassword.Salt;

            if (!await _usuarioRepository.CreateUsuarioAsync(usuario))
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<UsuarioResponse>(usuario);
            return Ok<Usuario, UsuarioResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<UsuarioResponse>> LockUsuarioAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(id);
            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            if (usuario.LockoutEnabled)
                return Error<UsuarioResponse>("La cuenta de este usuario se encuentra bloqueada.");

            return await _usuarioRepository.LockUsuarioAsync(id) ? Ok<UsuarioResponse>() : Error<UsuarioResponse>("No se pudo bloquear a ese usuario.");
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(id);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<UsuarioResponse>> GetUsuarioByEmailAsync(string email)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<UsuarioResponse>> GetUsuarioByUserNameAsync(string userName)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByUserNameAsync(userName);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosAsync()
    {
        try
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();

            var usuariosDto = new List<UsuarioResponse>();

            foreach (var usuario in usuarios)
            {
                usuariosDto.Add(_mapper.Map<UsuarioResponse>(usuario));
            }
            return Ok<ICollection<UsuarioResponse>>(usuariosDto);
        }
        catch (Exception ex)
        {
            return Error<Usuario, ICollection<UsuarioResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosByTipoAsync(UsuarioTipo tipo)
    {
        try
        {
            var usuarios = await _usuarioRepository.GetUsuariosByTipoAsync(tipo);

            var usuariosDto = new List<UsuarioResponse>();

            foreach (var usuario in usuarios)
            {
                usuariosDto.Add(_mapper.Map<UsuarioResponse>(usuario));
            }
            return Ok<ICollection<UsuarioResponse>>(usuariosDto);
        }
        catch (Exception ex)
        {
            return Error<Usuario, ICollection<UsuarioResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<LoginResponse>> LoginUsuarioAsync(LoginRequest request)
    {
        try
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioByEmailAsync(request.Email);

            if (usuario == null)
                return KeyError<Usuario, LoginResponse>(nameof(request.Email), GexErrorMessage.InvalidEmail);

            if (usuario.LockoutEnabled)
                return Error<LoginResponse>("Esta cuenta se encuentra bloqueada.");

            if(HashingExtensions.CheckHash(request.Password, usuario.Password, usuario.Salt))
            {
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));
                claims.AddClaim(new Claim(ClaimTypes.Role, usuario.Tipo.ToString()));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);

                return Ok(new LoginResponse() { Token = bearer_token });
            }

            return KeyError<Usuario, LoginResponse>(nameof(request.Password), GexErrorMessage.InvalidPassword);
        }
        catch(Exception ex)
        {
            return Error<Usuario, LoginResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosAsync(string email)
    {
        try
        {
            var usuarios = await _usuarioRepository.GetUsuariosByEmailAsync(email);

            var usuariosDto = new List<UsuarioResponse>();

            foreach (var usuario in usuarios)
            {
                usuariosDto.Add(_mapper.Map<UsuarioResponse>(usuario));
            }
            return Ok<ICollection<UsuarioResponse>>(usuariosDto);
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
