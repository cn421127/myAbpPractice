using Abp.Application.Services;

namespace myAbpBasic
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class myAbpBasicAppServiceBase : ApplicationService
    {
        protected myAbpBasicAppServiceBase()
        {
            LocalizationSourceName = myAbpBasicConsts.LocalizationSourceName;
        }
    }
}