using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.AccessTokens;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector
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
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            var tokenServerConfig = Configuration.GetSection("TokenServerConfig").Get<TokenServerConfig>();
            var evaluationApiConfig = Configuration.GetSection("EvaluationApiConfig").Get<EvaluationApiConfig>();
            services
                .AddSingleton(tokenServerConfig)
                .AddSingleton(evaluationApiConfig);

            services.AddHttpClient<IEvaluationApiClient, EvaluationApiClient>();
            services.AddHttpClient<ITokenClient, TokenClient>();
            services.AddSingleton<StatusUpdateService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Partner API Connector", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Partner API Connector");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", t =>
                {
                    t.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}
