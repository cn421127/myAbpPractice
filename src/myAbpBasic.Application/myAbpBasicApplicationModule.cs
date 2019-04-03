using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myAbpBasic.Interceptors;

namespace myAbpBasic
{
    [DependsOn(
        typeof(myAbpBasicCoreModule),
        typeof(AbpAutoMapperModule))]
    public class myAbpBasicApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            MeasureDurationInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicApplicationModule).GetAssembly());
        }

    }
}