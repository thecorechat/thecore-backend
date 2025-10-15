using Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Infrastructure_Tests
{
    public class DbContextTests
    {
        [Fact]
        public void recieveConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets<SchoolChatContext>().Build();
            var connString = configuration["ConnectionStrings:DefaultConnection"];
            Assert.True(connString is not null, "Connection string is null");
        }
    }
}
