using Abp.Application.Services;
using Abp.Application.Services.Dto;
using myAbpBasic.Tasks.Dto;
using System.Threading.Tasks;

namespace myAbpBasic.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);

        System.Threading.Tasks.Task Create(CreateTaskInput input);
    }


}
