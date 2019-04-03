using Abp.Modules;
using Abp.Reflection.Extensions;

namespace myAbpBasic.Business.Core
{
    public class myAbpBasicBusinessCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicBusinessCoreModule).GetAssembly());
        }

    }
}
