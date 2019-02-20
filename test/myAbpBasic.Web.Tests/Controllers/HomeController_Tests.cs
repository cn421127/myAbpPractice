using System.Threading.Tasks;
using myAbpBasic.Web.Controllers;
using Shouldly;
using Xunit;

namespace myAbpBasic.Web.Tests.Controllers
{
    public class HomeController_Tests: myAbpBasicWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
