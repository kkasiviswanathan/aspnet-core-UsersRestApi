using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UsersRestApi.Data;
using UsersRestApi.Helpers;
using UsersRestApi.Services;

namespace UsersRestApi
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
      services.AddDbContext<UsersContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("UsersDbConnection")));

      services.AddScoped<IUserService, UserService>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddSwaggerDocument(config =>
      {
        config.PostProcess = document =>
        {
          document.Info.Version = "v1";
          document.Info.Title = "Users API";
          document.Info.Description = "A simple ASP.NET Core web API to get User information";
          document.Info.TermsOfService = "None";
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        // Adding Latency simulation to the request pipeline
        app.UseLatencySimulation(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(500));

        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseOpenApi();
      app.UseSwaggerUi3();

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
