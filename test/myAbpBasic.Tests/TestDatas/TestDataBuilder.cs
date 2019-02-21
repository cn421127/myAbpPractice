using myAbpBasic.EntityFrameworkCore;
using myAbpBasic.Tasks;

namespace myAbpBasic.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly myAbpBasicDbContext _context;

        public TestDataBuilder(myAbpBasicDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
            _context.Tasks.AddRange(
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality."),
                new Task("Clean your room") { State = TaskState.Completed }
            );
        }
    }
}