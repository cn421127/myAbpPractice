using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using myAbpBasic.Configuration;
using myAbpBasic.EntityFrameworkCore;

namespace myAbpBasic.Web.Startup
{
    [DependsOn(
        typeof(myAbpBasicApplicationModule),
        typeof(myAbpBasicEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule)
        )]
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
            
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(myAbpBasicApplicationModule).GetAssembly(),"app"
                );
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicWebModule).GetAssembly());
        }
    }
}