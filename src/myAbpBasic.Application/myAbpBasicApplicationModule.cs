using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace myAbpBasic
{
    [DependsOn(
        typeof(myAbpBasicCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class myAbpBasicApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicApplicationModule).GetAssembly());
        }
    }
}