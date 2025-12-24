using Domain.Models;
using Infrastructure.DB;
using Infrastructure_Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DB
{
    public static class DbContextExtensions
    {
        public static void SeedData(this TestSchoolChatContext context)
        {
            if (context.Users.Any() || context.Chats.Any())
                return;

            var users = new[]
            {
                new User { Username = "admin_user", AzureAdObjectId = Guid.NewGuid().ToString(), Identifier = Guid.NewGuid().ToString()},
                new User { Username = "teacher_smith", AzureAdObjectId = Guid.NewGuid().ToString(), Identifier = Guid.NewGuid().ToString()},
                new User { Username = "student_john", AzureAdObjectId = Guid.NewGuid().ToString(), Identifier = Guid.NewGuid().ToString()}
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var chats = new[]
            {
                new Chat { Title = "General School Chat" },
                new Chat { Title = "Math Class 10-A" }
            };

            context.Chats.AddRange(chats);
            context.SaveChanges();

            var mathChatId = chats.Single(c => c.Title == "Math Class 10-A").Id;

            var messages = new[]
            {
                new Message
                {
                    ChatId = mathChatId,
                    Author = "teacher_smith",
                    Text = "Hello class!"
                }
            };

            context.Messages.AddRange(messages);
            context.SaveChanges();

            var permissions = context.Permissions.ToList();

            var chatPermissions = new[]
            {
                new ChatUserPermission
                {
                    ChatId = mathChatId,
                    UserId = users.Single(u => u.Username == "teacher_smith").Id,
                    PermissionId = permissions.Single(p => p.Name == "ReadMessages").Id
                }
            };

            context.ChatUserPermissions.AddRange(chatPermissions);
            context.SaveChanges();
        }

    }
}
