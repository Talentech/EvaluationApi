using Everest.SampleAssessment.AssessmentProviders;
using Everest.SampleAssessment.AssessmentProviders.Interfaces;
using Everest.SampleAssessment.Extensions;
using Everest.SampleAssessment.Models.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Everest.SampleAssessment
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

            services.Configure<AssessmentApiConfiguration>(Configuration.GetSection("AssessmentFrameworkApiBaseUrl"));
            services.Configure<IdentityServerConfiguration>(Configuration.GetSection("Authentication"));


            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
        
            ServiceClassExtensions.AddHttpClient(services);
            services.AddSingleton<IApiClientCallback, ApiClientCallback>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleAssessmentHandler", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example assesment provider handler Api");
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
            app.UseMvc();
        }
    }
}
