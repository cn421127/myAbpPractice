using Abp.AspNetCore.Mvc.Views;

namespace myAbpBasic.Web.Views
{
    public abstract class myAbpBasicRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected myAbpBasicRazorPage()
        {
            LocalizationSourceName = myAbpBasicConsts.LocalizationSourceName;
        }
    }
}
