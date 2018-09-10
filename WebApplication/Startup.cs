using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Utility;
using WebApplication.Swagger;

namespace WebApplication
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityConfiguration = _configuration.Get<IdentityConfiguration>();
            services.AddSingleton(identityConfiguration);

            services.AddMvc()
                // Points to a controller in another project.
                .ConfigureApplicationPartManager(applicationPartManager =>
                {
                    applicationPartManager.ApplicationParts.Clear();
                    applicationPartManager.ApplicationParts.Add(new AssemblyPart(typeof(DataAccess.ValuesController).Assembly));
                });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info {Version = "v1", Title = "v1 API"});
                options.SwaggerDoc("v2", new Info {Version = "v2", Title = "v2 API"});
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
                options.OperationFilter<AuthorizeOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication(); // must be before mvc

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
            });

            app.UseMvc();
        }
    }
}