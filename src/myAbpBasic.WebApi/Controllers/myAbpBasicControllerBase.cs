using Abp.AspNetCore.Mvc.Controllers;

namespace myAbpBasic.Web.Controllers
{
    public abstract class myAbpBasicControllerBase: AbpController
    {
        protected myAbpBasicControllerBase()
        {
            LocalizationSourceName = myAbpBasicConsts.LocalizationSourceName;
        }
    }
}