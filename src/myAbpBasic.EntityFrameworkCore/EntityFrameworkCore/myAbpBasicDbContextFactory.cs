using myAbpBasic.Configuration;
using myAbpBasic.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace myAbpBasic.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class myAbpBasicDbContextFactory : IDesignTimeDbContextFactory<myAbpBasicDbContext>
    {
        public myAbpBasicDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<myAbpBasicDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(myAbpBasicConsts.ConnectionStringName)
            );

            return new myAbpBasicDbContext(builder.Options);
        }
    }
}