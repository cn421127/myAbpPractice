using myAbpBasic.People;
using myAbpBasic.People.Dto;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace myAbpBasic.Tests.Person
{
    public class PersonAppService_Tests : myAbpBasicTestBase
    {
        private readonly IPersonAppService _personAppService;

        public PersonAppService_Tests()
        {
            _personAppService = Resolve<IPersonAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Persons()
        {
            var output = await _personAppService.GetAll((new GetAllPersonInput()));

            output.Items.Count.ShouldBe(2);
            output.Items.Count(t => t.Name == "PP").ShouldBe(1);
        }

        [Fact]
        public async Task Should_Create_New_Person()
        {
            await _personAppService.Create(new CreatePersonInput
            {
                Name = "NewPerson"
            });

            UsingDbContext(context =>
            {
                var t1 = context.People.FirstOrDefault(t => t.Name == "NewPerson");
                t1.ShouldNotBeNull();
            });
        }
    }
}
