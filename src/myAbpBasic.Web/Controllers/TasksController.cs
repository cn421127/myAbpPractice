using Microsoft.AspNetCore.Mvc;
using myAbpBasic.Tasks;
using System.Threading.Tasks;
using myAbpBasic.Web.Models;

namespace myAbpBasic.Web.Controllers
{
    public class TasksController : myAbpBasicControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);
            var model = new IndexViewModel(output.Items);
            return View(model);
        }
    }
}