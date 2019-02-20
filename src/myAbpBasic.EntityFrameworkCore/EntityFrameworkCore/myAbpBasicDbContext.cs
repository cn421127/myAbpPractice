using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myAbpBasic.EntityFrameworkCore
{
    public class myAbpBasicDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public myAbpBasicDbContext(DbContextOptions<myAbpBasicDbContext> options) 
            : base(options)
        {

        }
    }
}
