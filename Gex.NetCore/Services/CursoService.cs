using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Gex.NetCore.Models;
using Gex.NetCore.Services.Interface;
using Gex.NetCore.ViewModels;
using Gex.NetCore.ViewModels.Curso;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gex.NetCore.Services
{
    public class CursoService : ICursoService
    {
        private readonly IMapper _mapper;
        private readonly GexContext _context;

        public CursoService(IMapper mapper, GexContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<List<CursoCreationDTO>> AllAsync()
        {
            var cursos = await _context.Cursos.ToListAsync();
            return _mapper.Map<List<CursoCreationDTO>>(cursos);
        }

        public async Task<long> CreateAsync(CursoDTO cursoDTO)
        {
            var curso = _mapper.Map<Curso>(cursoDTO);
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso.Id;
        }

        public async Task<CursoDTO> DeleteAsync(long id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
                return null;

            _context.Cursos.Remove(curso);
            return _mapper.Map<CursoDTO>(curso);
        }

        public async Task<CursoDTO> ModifyAsync(long id, CursoDTO cursoDTO)
        {
            if (!Exists(id))
                return null;

            var curso = _mapper.Map<Curso>(cursoDTO);
            curso.Id = id;
            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return await FindAync(id);
        }

        public async Task<CursoDTO> FindAync(long id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            return _mapper.Map<CursoDTO>(curso);
        }

        public bool Exists(long id) => _context.Cursos.Any(e => e.Id == id);
        

    }
}
