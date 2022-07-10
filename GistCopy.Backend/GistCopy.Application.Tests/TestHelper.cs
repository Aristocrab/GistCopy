using GistCopy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GistCopy.Application.Tests.Common;

public class TestHelper
{
    public static Guid PublicGistId => Guid.Parse("B027B80C-A0B5-4311-8360-50637092934A");
    public static Guid PrivateGistId => Guid.Parse("15F7BB0E-6654-4744-BFC1-F4805375FD70");
    
    public static Guid PublicGistOwnerId => Guid.Parse("E2B13928-873C-49A9-9D0C-1AD48A4E0533");
    public static Guid PrivateGistOwnerId => Guid.Parse("91CAF717-1A6A-4836-B615-67F4991D99EB");
    
    public static Guid UnexistingGuid => Guid.Parse("BDEFA020-4809-4934-BD79-B10440EF53E2");

    public static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        
        // Seed
        var userA = new User
        {
            Id = PublicGistOwnerId,
            Username = "Alice",
            Password = "qwerty123"
        };
        var userB = new User
        {
            Id = PrivateGistOwnerId,
            Username = "Bob",
            Password = "password"
        };
        context.Users.AddRange(userA, userB);

        var publicGist = new Gist
        {
            Id = PublicGistId,
            User = userA,
            Description = "Test description",
            Filename = "text.txt",
            Private = false,
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi non quis exercitationem culpa nesciunt nihil aut nostrum explicabo reprehenderit optio amet ab temporibus asperiores quasi cupiditate."
        };
        var privateGist = new Gist
        {
            Id = PrivateGistId,
            User = userB,
            Description = "A private one",
            Filename = "private.md",
            Private = true,
            Text = "Ad dolore dignissimos asperiores dicta facere optio quod commodi nam tempore recusandae."
        };
        context.Gists.AddRange(publicGist, privateGist);
        
        context.Comments.AddRange(
            new Comment
            {
                Id = Guid.Parse("B9B1284F-DBE7-4234-8FD2-4BC34A20EC9D"),
                User = userA,
                Text = "Nice gist",
                Gist = publicGist
            },
            new Comment
            {
                Id = Guid.Parse("B5B74046-306E-4113-9581-AA1E76854C23"),
                User = userB,
                Text = "Good gist",
                Gist = publicGist
            },
            new Comment
            {
                Id = Guid.Parse("144A1A68-313A-49C0-9EBD-E6355C9CA151"),
                User = userB,
                Text = "My private gist is awesome",
                Gist = privateGist
            }
        );

        return context;
    }
}