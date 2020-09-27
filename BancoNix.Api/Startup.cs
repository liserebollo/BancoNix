using BancoNix.Aplicacao;
using BancoNix.Infra.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BancoNix.Api
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
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddScoped<ITransferenciaService, TransferenciaService>();

            services.AddSingleton(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();

                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Padrao"));

                var banco = new BancoContext(optionsBuilder.Options);

                banco.Seed();

                return banco;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Avaliação Situacional",
                    Description = "ASP.NET Core Web API",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "Liseane Iesbick",
                        Email = "rebollodeveloper@gmail.com",
                        //Url = new Uri(""),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsApi");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
