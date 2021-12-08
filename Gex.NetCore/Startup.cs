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
using Gex.Models;
using Gex.Middlewares;
using Microsoft.OpenApi.Models;

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
        /* INYECCION DE SERVICIOS */
        services.AddScoped<IComisionRepository, ComisionRepository>();
        services.AddScoped<IExamenService, ExamenService>();
        services.AddScoped<IMateriaService, MateriaService>();

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
        }).AddBadRequestServices();
        services.AddDbContext<GexContext>(options =>
        {
            options.UseMySql(Configuration.GetValue<string>("DatabaseConnection"), new MariaDbServerVersion(new Version(10, 4, 17)), o =>
            {
                o.EnableRetryOnFailure();
            });
            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableSensitiveDataLogging();
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
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gex",
                Version = "v1"
            });
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
