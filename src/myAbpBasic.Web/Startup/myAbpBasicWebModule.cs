using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myAbpBasic.Configuration;
using myAbpBasic.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace myAbpBasic.Web.Startup
{
    [DependsOn(
        typeof(myAbpBasicApplicationModule), 
        typeof(myAbpBasicEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class myAbpBasicWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public myAbpBasicWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(myAbpBasicConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<myAbpBasicNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(myAbpBasicApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicWebModule).GetAssembly());
        }
    }
}