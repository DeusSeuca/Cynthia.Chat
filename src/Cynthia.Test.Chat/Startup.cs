using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cynthia.Test.Chat.Attributes;
using System.Reflection;

namespace Cynthia.Test.Chat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var myControllers = assembly.ExportedTypes.Where(x => x.Name.EndsWith("Controller") && x.IsClass && !x.IsAbstract && !x.ContainsGenericParameters).ToArray();
            var myServices = assembly.DefinedTypes.Where(x => (x.IsDefined(typeof(ServiceAttribute)))
            || (!x.IsDefined(typeof(NonServiceAttribute)) && x.Name.EndsWith("Service") && x.IsClass && !x.IsAbstract && !x.ContainsGenericParameters)).ToArray();

            services.AddMvc().AddControllersAsServices();
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //控制器
            builder.RegisterTypes(myControllers).PropertiesAutowired();
            //服务
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(SingletonAttribute))).ToArray()).AsImplementedInterfaces().PropertiesAutowired().SingleInstance();
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(TransientAttribute))).ToArray()).AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(ScopedAttribute))).ToArray()).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();

            ApplicationContainer = builder.Build();
            services.AddMvc();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
