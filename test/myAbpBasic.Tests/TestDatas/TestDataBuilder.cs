using myAbpBasic.EntityFrameworkCore;
using myAbpBasic.People;
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
            var neo = new People.Person("Neo");
            var pp = new People.Person("PP");
            _context.People.Add(neo);
            _context.People.Add(pp);
            _context.SaveChanges();

            _context.Tasks.AddRange(
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality.", neo.Id),
                new Task("Clean your room") {State = TaskState.Completed}
            );
        }
    }
}