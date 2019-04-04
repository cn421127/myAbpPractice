using Abp.Application.Services;
using Abp.Application.Services.Dto;
using myAbpBasic.Tasks.Dto;
using System.Threading.Tasks;
using System.Web.Http;

namespace myAbpBasic.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        [HttpGet]
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);

        System.Threading.Tasks.Task Create(CreateTaskInput input);
    }


}
