using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using myAbpBasic.People.Dto;

namespace myAbpBasic.People
{
    public interface IPersonAppService : IApplicationService
    {
        Task<ListResultDto<PersonListDto>> GetAll(GetAllPersonInput input);

        System.Threading.Tasks.Task Create(CreatePersonInput input);
    }
}
