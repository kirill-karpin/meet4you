using AutoMapper;
using Location.City.Mapping;
using Location.Country.Mapping;
using Location.UserLocation.Mapping;
using Message;
using Message.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using User.Mapping;

namespace WebApp
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
            InstallAutomapper(services);
            services.AddServices(Configuration);
            services.AddControllers();
            services.AddCors();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        RefreshBeforeValidation = true
                    };
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static IServiceCollection InstallAutomapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMappingProfile>();
                cfg.AddProfile<CityMappingProfile>();
                cfg.AddProfile<CountryMappingProfile>();
                cfg.AddProfile<UserLocationMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}