using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Tests
{
    public class TestSchoolChatContext : SchoolChatContext
    {
        public TestSchoolChatContext(DbContextOptions<SchoolChatContext> options)
            : base(options)
        {
        }
    }

}
