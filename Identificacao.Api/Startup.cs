using Identificacao.Dominio.Manipuladores;
using Identificacao.Dominio.Repositorios;
using Identificacao.Infra.Contexto;
using Identificacao.Infra.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.IO;
using System;
using Microsoft.OpenApi.Models;

namespace Identificacao.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddResponseCompression();

            // Conexão com o banco 
            //services.AddDbContext<DadosContexto>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DadosContexto>(opt =>
                opt.UseMySql(
                    Configuration.GetConnectionString("conn"),
                    x => x.ServerVersion("10.5.4-mariadb")));

            //Dependecias repositorios
            services.AddTransient<IRepositorioUsuario, RepositorioUsuarios>();
            services.AddTransient<IRepositorioEmpresa, RepositorioEmpresa>();

            //Dependencia Manipuladores
            services.AddTransient<ManipuladorUsuario, ManipuladorUsuario>();
            services.AddTransient<ManipuladorEmpresa, ManipuladorEmpresa>();

            // Configura documentacao
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Serviço de Identificação",
                    Version = "v1",
                    Description = "Serviço responsável pelo cadastro de novos usuario, empresas e login na ferramenta",
                    Contact = new OpenApiContact
                    {
                        Name = "Soft Clever",
                        Email = "softclever@softclever.com.br",
                        Url = new Uri("https://www.softclever.com.br/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            //Consiguração da aplicação para IIS
            services.Configure<IISServerOptions>(x =>
            {
                x.AutomaticAuthentication = false;
                x.MaxRequestBodySize = 30000000;
            });
            services.Configure<IISOptions>(x =>
            {
                x.ForwardClientCertificate = false;
            });
            services.AddApplicationInsightsTelemetry();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço de Identificação");
                x.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
