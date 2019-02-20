using Microsoft.EntityFrameworkCore;

namespace myAbpBasic.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<myAbpBasicDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for myAbpBasicDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
