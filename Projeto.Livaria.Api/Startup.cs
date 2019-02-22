using App.Metrics.Health;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Dados.Repositorios;
using Projeto.Livraria.Dados.Source;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Livaria.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            InitializeDependencyInjection(services);

            services.AddAutoMapper();

            services.AddResponseCompression();

            var metrics = AppMetricsHealth.CreateDefaultBuilder()
                .HealthChecks.RegisterFromAssembly(services)
                .BuildAndAddTo(services);

            services.AddHealth(metrics);
            services.AddHealthEndpoints();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling =
                       Newtonsoft.Json.NullValueHandling.Ignore;
            });

            ResponseValidationErrorHandler(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api Livros", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
        }

        private static void ResponseValidationErrorHandler(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = 400,
                        Message = "Erros de validação",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

        private void InitializeDependencyInjection(IServiceCollection services)
        {
            var connection = Configuration["MySqlConnection:MysqlConnectionString"];
            services.AddDbContext<MySqlContext>(opt => opt.UseMySQL(connection));

            services.AddScoped(typeof(IRepositorio<,>), typeof(Repositorio<,>));
            services.AddScoped<ILivroRepositorio, LivroRepositorio>();      
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                InitializeDatabase();

                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Livros v1");
            });

            app.UseHealthAllEndpoints();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitializeDatabase()
        {
            try
            {
                var evolveConnection = new MySqlConnection(Configuration["MySqlConnection:MysqlConnectionString"]);
                var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                {
                    Locations = new List<string> { "db/migrations" },
                    IsEraseDisabled = true

                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Database migration failed.", ex);
                throw;
            }

        }
    }
}
