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
using Gex.NetCore.LocalizationResources;
using System.Globalization;
using XLocalizer.Routing;
using XLocalizer.Xml;
using Gex.NetCore.Services.Interface;
using Gex.NetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gex.NetCore.Repository.Interface;
using Gex.NetCore.Repository;
using EntityFramework.Exceptions.MySQL.Pomelo;
using Gex.NetCore.Middlewares;
using Gex.NetCore.Filters;

namespace Gex.NetCore;

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
        /* INYECCION DE SERVICIOS */
        services.AddScoped<IComisionService, ComisionService>();

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
                ValidateAudience = false
            };
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
