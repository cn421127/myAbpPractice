using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace myAbpBasic.EntityFrameworkCore
{
    [DependsOn(
        typeof(myAbpBasicCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class myAbpBasicEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicEntityFrameworkCoreModule).GetAssembly());
        }
    }
}