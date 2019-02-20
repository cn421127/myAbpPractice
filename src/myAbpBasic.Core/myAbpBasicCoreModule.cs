using Abp.Modules;
using Abp.Reflection.Extensions;
using myAbpBasic.Localization;

namespace myAbpBasic
{
    public class myAbpBasicCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            myAbpBasicLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myAbpBasicCoreModule).GetAssembly());
        }
    }
}