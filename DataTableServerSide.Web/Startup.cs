using AutoMapper;
using DatatableServerSide.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DatatableServerSide.Repository.Interface;
using DatatableServerSide.Repository.Implementation;
using DatatableServerSide.Service.Interface;
using DatatableServerSide.Service.Implementations;

namespace DataTableServerSide.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddAutoMapper(typeof(Startup));

            var datatableOptions = new DataTables.AspNet.AspNetCore.Options()
                                   .EnableRequestAdditionalParameters()
                                   .EnableResponseAdditionalParameters();

            var dataTableBinder = new ModelBinder();
            dataTableBinder.ParseAdditionalParameters = Parser;
            services.RegisterDataTables(datatableOptions, dataTableBinder);

            services.AddDbContext<UserDataDbContext>(options =>
                      options.UseSqlServer(Configuration.GetConnectionString("UserDataDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private static IDictionary<string, object> Parser(ModelBindingContext arg)
        {
            var res = new Dictionary<string, object>();
            var httpMethod = arg.HttpContext.Request.Method;

            var keys = (httpMethod.ToLower() == "post") ? arg.HttpContext.Request.Form.Keys : arg.HttpContext.Request.Query.Keys;
            var modelKeys = keys.Where(m => !m.StartsWith("columns") && !m.StartsWith("order") && !m.StartsWith("search") && m != "draw" && m != "start" && m != "length" && m != "_");
            foreach (string key in modelKeys)
            {
                var value = arg.ValueProvider.GetValue(key).FirstValue;
                if (value.Length > 0)
                    res.Add(key, value);
            }
            return res;
        }
    }
}
