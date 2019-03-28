using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using myAbpBasic.People.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace myAbpBasic.People
{
    public class PersonAppService : myAbpBasicAppServiceBase, IPersonAppService
    {
        private readonly IRepository<Person, Guid> _personRepository;

        public PersonAppService(IRepository<Person, Guid> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task Create(CreatePersonInput input)
        {
            var person = ObjectMapper.Map<Person>(input);
            await _personRepository.InsertAsync(person);
        }

        public async Task<ListResultDto<PersonListDto>> GetAll(GetAllPersonInput input)
        {
            var persons = await _personRepository
                .GetAll()
                .WhereIf(!input.Name.IsNullOrEmpty(), t => t.Name.Contains(input.Name))
                .OrderByDescending(t => t.CreationTime)
                .ToDynamicListAsync();

            return new ListResultDto<PersonListDto>(ObjectMapper.Map<List<PersonListDto>>(persons));
        }
    }
}
