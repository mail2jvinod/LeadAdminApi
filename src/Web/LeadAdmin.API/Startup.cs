using LeadAdmin.API.Helpers;
using LeadAdmin.API.Security;
using LeadAdmin.Entities.Helpers;
using LeadAdmin.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace LeadAdmin.API
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();

            var settingsSection = Configuration.GetSection("AppSettings");
            ConfigSettings.Instance = settingsSection.Get<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //Invoke DI
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            DIContainer.ServiceLocator.Instance.LoadContainer(services);

            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            // Add framework services.
            services.AddMvc(options => { options.EnableEndpointRouting = false; }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            var tokenProvider = new RsaJwtTokenProvider();
            services.AddSingleton<ITokenProvider>(tokenProvider);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = ConfigSettings.Instance.ConfigSettings.Auth_ValidIssuer,
                        ValidAudience = ConfigSettings.Instance.ConfigSettings.Auth_ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettings.Instance.ConfigSettings.Auth_IssuerSigningKey)) //Secret
                    };

                });

            // This is for the [Authorize] attributes.
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddMemoryCache();
            services.AddControllers();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lead API", Version = "v1.0.2.2" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseDeveloperExceptionPage();
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VFhiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtScERgWXteeHNdQ2A=");

#if DEBUG
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                //var configuration = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
                //configuration.DisableTelemetry = true;
            }
#endif
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lead V1");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseUserInfo();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
