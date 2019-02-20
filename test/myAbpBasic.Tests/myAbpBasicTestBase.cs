using System;
using System.Threading.Tasks;
using Abp.TestBase;
using myAbpBasic.EntityFrameworkCore;
using myAbpBasic.Tests.TestDatas;

namespace myAbpBasic.Tests
{
    public class myAbpBasicTestBase : AbpIntegratedTestBase<myAbpBasicTestModule>
    {
        public myAbpBasicTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<myAbpBasicDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<myAbpBasicDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<myAbpBasicDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<myAbpBasicDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<myAbpBasicDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<myAbpBasicDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<myAbpBasicDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<myAbpBasicDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
