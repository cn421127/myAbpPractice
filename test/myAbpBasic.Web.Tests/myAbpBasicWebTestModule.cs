using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myAbpBasic.Web.Startup;
namespace myAbpBasic.Web.Tests
{
    [DependsOn(
        typeof(myAbpBasicWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class myAbpBasicWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicWebTestModule).GetAssembly());
        }
    }
}