﻿using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myAbpBasic.People;
using myAbpBasic.Tasks;

namespace myAbpBasic.EntityFrameworkCore
{
    public class myAbpBasicDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Person> People { get; set; }

        public myAbpBasicDbContext(DbContextOptions<myAbpBasicDbContext> options)
            : base(options)
        {

        }
    }
}
