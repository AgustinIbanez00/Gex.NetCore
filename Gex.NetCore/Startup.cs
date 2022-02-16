using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using XLocalizer;
using Gex.LocalizationResources;
using System.Globalization;
using XLocalizer.Routing;
using XLocalizer.Xml;
using Gex.Services.Interface;
using Gex.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gex.Repository.Interface;
using Gex.Repository;
using EntityFramework.Exceptions.MySQL.Pomelo;
using Gex.Filters;
using Gex.Middlewares;
using Microsoft.OpenApi.Models;
using Gex.Utils;
using Gex.Models.Enums;
using System.Reflection;
using System.IO;

namespace Gex;

public class Startup
{
    private string[] supportedCultures = new string[]
    {
            "es-ES", "en-US"
    };

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

        services.AddAutoMapper(typeof(Startup));

        /* INYECCION DE REPOSITORIOS */
        services.AddScoped<IComisionRepository, ComisionRepository>();
        services.AddScoped<IExamenRepository, ExamenRepository>();
        services.AddScoped<IMateriaRepository, MateriaRepository>();
        services.AddScoped<IMesaExamenRepository, MesaExamenRepository>();
        services.AddScoped<IPreguntaRepository, PreguntaRepository>();
        services.AddScoped<IRespuestaRepository, RespuestaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IInscripcionMesaRepository, InscripcionMesaRepository>();
        /* INYECCION DE SERVICIOS */
        services.AddScoped<IComisionService, ComisionService>();
        services.AddScoped<IExamenService, ExamenService>();
        services.AddScoped<IMateriaService, MateriaService>();
        services.AddScoped<IMesaExamenService, MesaExamenService>();
        services.AddScoped<IPreguntaService, PreguntaService>();
        services.AddScoped<IRespuestaService, RespuestaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IInscripcionMesaService, InscripcionMesaService>();

        services.AddCors();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ProfesoresOnly",
                 policy => policy.RequireRole(nameof(UsuarioTipo.Administrador), nameof(UsuarioTipo.Profesor)));
            options.AddPolicy("AlumnosOnly",
                 policy => policy.RequireRole(nameof(UsuarioTipo.Administrador), nameof(UsuarioTipo.Alumno)));
            options.AddPolicy("AdministratorsOnly",
                 policy => policy.RequireRole(nameof(UsuarioTipo.Administrador)));
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<HttpResponseExceptionFilter>();
        }).AddBadRequestServices()
           .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy());

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddDbContext<GexContext>(options =>
        {
#if DEBUG
            options.UseMySql(Configuration.GetValue<string>("DatabaseConnection"), new MariaDbServerVersion(new Version(10, 4, 17)), o =>
            {
                o.EnableRetryOnFailure();
            });
            options.EnableSensitiveDataLogging();
#else
            options.UseInMemoryDatabase("Gex");
#endif

            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableDetailedErrors();
            options.UseExceptionProcessor();
            options.UseSnakeCaseNamingConvention();



        });
        services.Configure<RequestLocalizationOptions>(ops =>
        {
            var cultures = new CultureInfo[] { new CultureInfo("en"), new CultureInfo("es"), new CultureInfo("ar") };
            ops.SupportedCultures = cultures;
            ops.SupportedUICultures = cultures;
            ops.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");

            ops.RequestCultureProviders.Insert(0, new RouteSegmentRequestCultureProvider(cultures));
        });
        services.AddSingleton<IXResourceProvider, XmlResourceProvider>();

        services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddMvc()
            .AddDataAnnotationsLocalization()
            .AddXLocalizer<LocSource>(ops => Configuration.GetSection("XLocalizerOptions").Bind(ops))
        ;
        services.AddSwaggerDocument(options =>
        {
            options.Description = "Colleción de API's correspondientes al sistema de exámenes Gex.";
            options.Title = "Sistema de Exámenes Gex";
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gex",
                Version = "v1",
                Description = "Colleción de API's correspondientes al sistema de exámenes Gex.",
                Contact = new OpenApiContact() { Email = "admin@gexsystem.com", Name = "Agustin Ibañez" }

            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseOpenApi();
        app.UseSwaggerUi3(options =>
        {
            options.DocumentTitle = "Sistema de Exámenes Gex";
        });

    }
}
