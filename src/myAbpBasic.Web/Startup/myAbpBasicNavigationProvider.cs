﻿using Abp.Application.Navigation;
using Abp.Localization;

namespace myAbpBasic.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class myAbpBasicNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "Home/About",
                        icon: "fa fa-info"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "TaskList",
                        L("TaskList"),
                        url: "Tasks",
                        icon: "fa fa-tasks"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "PersonList",
                        L("PersonList"),
                        url: "Person",
                        icon: "fa fa-tasks"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, myAbpBasicConsts.LocalizationSourceName);
        }
    }
}
