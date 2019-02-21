﻿using Microsoft.AspNetCore.Mvc;
using myAbpBasic.Tasks;
using myAbpBasic.Tasks.Dto;
using myAbpBasic.Web.Models;
using System.Threading.Tasks;
using myAbpBasic.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Abp.Application.Services.Dto;

namespace myAbpBasic.Web.Controllers
{
    public class TasksController : myAbpBasicControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ILookupAppService _lookupAppService;

        public TasksController(ITaskAppService taskAppService,
            ILookupAppService lookupAppService)
        {
            _taskAppService = taskAppService;
            _lookupAppService = lookupAppService;
        }

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);
            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskState = input.State,
            };
            return View(model);
        }


        public async Task<ActionResult> Create()
        {
            var peopleSelectListItems = (await _lookupAppService.GetPeopleComboboxItems()).Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            peopleSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            return View(new CreateTaskViewModel(peopleSelectListItems));
        }
    }
}