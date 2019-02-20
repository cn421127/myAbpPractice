using myAbpBasic.EntityFrameworkCore;

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
        }
    }
}