

using Domain.Models;
using FluentAssertions;
using Infrastructure.DB;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MR.AspNetCore.Pagination;

namespace Infrastructure_Tests
{
    public class ChatRepositoryTests
    {


        [Fact]
        public async Task GetChatKeysetPaginationAsync_ShouldReturnTwoChats_WhenLimitIsTwo()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddPagination()
                .BuildServiceProvider();
            
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolChatContext>()
                .UseInMemoryDatabase(databaseName: "ChatPaginationTest_" + Guid.NewGuid().ToString())
                .Options;

            using var context = new TestSchoolChatContext(options);
            context.Database.EnsureCreated();
            context.SeedData();
            var paginationService = serviceProvider.GetRequiredService<IPaginationService>();
            var repo = new ChatsQueryRepository(context, paginationService);
            var allChats = await context.Chats.ToListAsync();

            foreach (var chat in allChats)
            {
                Console.WriteLine($"Chat Id: {chat.Id}, Title: {chat.Title}");
            }


            context.Chats.Add(new Chat { Id = 3, Title = "History Class" });
            await context.SaveChangesAsync();

            // Act
            var result = await repo.GetChatKeysetPaginationAsync(
                after: null,
                propName: "Id",
                limit: 2,
                reverse: false
            );

            // Assert
            result.Data.Should().NotBeNull();
            result.Data.Count.Should().Be(2);

            result.Data.First().Id.Should().Be(1);
            result.Data.Last().Id.Should().Be(2);
            
        }

    }
}
