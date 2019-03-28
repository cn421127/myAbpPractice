using Microsoft.AspNetCore.Mvc;
using myAbpBasic.People;
using myAbpBasic.People.Dto;
using myAbpBasic.Web.Models.Person;
using System.Threading.Tasks;

namespace myAbpBasic.Web.Controllers
{
    public class PersonController : myAbpBasicControllerBase
    {
        private readonly IPersonAppService _personAppService;

        public PersonController(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<ActionResult> Index(GetAllPersonInput input)
        {
            var output = await _personAppService.GetAll(input);
            var model = new IndexViewModel(output.Items);


            return View(model);
        }
    }
}