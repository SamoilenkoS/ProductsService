using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsBusinessLayer.MapperProfiles;
using ProductsBusinessLayer.Services.AuthService;
using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsBusinessLayer.Services.HashService;
using ProductsBusinessLayer.Services.ProductService;
using ProductsBusinessLayer.Services.RegistrationService;
using ProductsBusinessLayer.Services.SmtpService;
using ProductsBusinessLayer.Services.UserService;
using ProductsCore.Models;
using ProductsCore.Options;
using ProductsDataLayer;
using ProductsDataLayer.Repositories.EmailRepository;
using ProductsDataLayer.Repositories.ProductRepository;
using ProductsDataLayer.Repositories.UserRepository;
using System.Reflection;

namespace ProductsPresentationLayer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<EFCoreContext>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:5002";
                options.InstanceName = "SampleInstance";
            });

            services.AddScoped<ISettingsService<ChatUserSettings>, ChatSettingsService>();

            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddDbContext<EFCoreContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            services.Configure<AuthOptions>(Configuration.GetSection(nameof(AuthOptions)));
            services.Configure<SmtpOptions>(Configuration.GetSection(nameof(SmtpOptions)));

            var authOptions = Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
                            (System.Text.Encoding.ASCII.GetBytes(authOptions.SecretKey)),
                        ValidateIssuerSigningKey = true
                    };
                });

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(ProductsProfile))
            };

            services.AddAutoMapper(assemblies);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepositoryDb>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ISmtpService, SmtpServiceMoq>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IEmailRepository, EmailRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<LoggerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapHealthChecks("/");
            });
        }
    }
}
