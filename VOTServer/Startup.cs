using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using VOTServer.Core.Interface;
using VOTServer.Core.Services;
using VOTServer.Infrastructure.Data;
using VOTServer.Infrastructure.Data.Repositories;
using VOTServer.Options;

namespace VOTServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetValue("JWT:Issuer", "VOT"),
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetValue("JWT:Audience", "VOT"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue("JWT:SigningKey", "XBxNkZ9RJvzZv5zzn3cAKl74DXbRzP0X"))),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                };
            });
            services.Configure<JWTOptions>(Configuration.GetSection("JWT"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VOTServer", Version = "v1" });
            });
            services.AddDbContext<VOTDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("VOTServer")));
            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            // Data Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VOTServer v1"));
            }
#if DEBUG
            app.UseHttpsRedirection();
#endif
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
