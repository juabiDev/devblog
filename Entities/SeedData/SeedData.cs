using System;
using System.Collections.Generic;
using Entities.Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities.SeedData;

public static class SeedData
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		Random random = new Random();
		List<User> list = new List<User>();
		PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
		for (int i = 1; i <= 30; i++)
		{
			User user = new User
			{
				Id = Guid.Parse($"00000000-0000-0000-0000-0000000000{i:D2}"),
				Name = $"User {i}",
				UserName = $"user{i}",
				Email = $"user{i}@example.com",
				CreatedAt = DateTime.UtcNow
			};
			user.PasswordHash = passwordHasher.HashPassword(user, "P@ssw0rd123");
			list.Add(user);
		}
		modelBuilder.Entity<User>().HasData(list.ToArray());
		List<Category> list2 = new List<Category>();
		for (int j = 1; j <= 30; j++)
		{
			list2.Add(new Category
			{
				Id = Guid.Parse($"10000000-0000-0000-0000-0000000000{j:D2}"),
				Name = $"Category {j}"
			});
		}
		modelBuilder.Entity<Category>().HasData(list2.ToArray());
		List<Tag> list3 = new List<Tag>();
		for (int k = 1; k <= 30; k++)
		{
			list3.Add(new Tag
			{
				Id = Guid.Parse($"20000000-0000-0000-0000-0000000000{k:D2}"),
				Name = $"Tag{k}"
			});
		}
		modelBuilder.Entity<Tag>().HasData(list3.ToArray());
		List<Post> list4 = new List<Post>();
		for (int l = 1; l <= 30; l++)
		{
			list4.Add(new Post
			{
				Id = Guid.Parse($"30000000-0000-0000-0000-0000000000{l:D2}"),
				Title = $"Post Title {l}",
				Content = $"Content of post {l}",
				CreatedAt = DateTime.UtcNow.AddDays(-random.Next(0, 365)),
				AuthorId = list[random.Next(list.Count)].Id,
				CategoryId = list2[random.Next(list2.Count)].Id
			});
		}
		modelBuilder.Entity<Post>().HasData(list4.ToArray());
		List<Comment> list5 = new List<Comment>();
		for (int m = 1; m <= 30; m++)
		{
			Post post = list4[random.Next(list4.Count)];
			list5.Add(new Comment
			{
				Id = Guid.Parse($"40000000-0000-0000-0000-0000000000{m:D2}"),
				Text = $"Comment text {m}",
				CreatedAt = DateTime.UtcNow.AddDays(-random.Next(0, 365)),
				UpdatedAt = DateTime.UtcNow,
				AuthorId = list[random.Next(list.Count)].Id,
				PostId = post.Id
			});
		}
		modelBuilder.Entity<Comment>().HasData(list5.ToArray());
		List<Follow> list6 = new List<Follow>();
		HashSet<(Guid, Guid)> hashSet = new HashSet<(Guid, Guid)>();
		while (list6.Count < 30)
		{
			User user2 = list[random.Next(list.Count)];
			User user3 = list[random.Next(list.Count)];
			if (!(user2.Id == user3.Id))
			{
				(Guid, Guid) item = (user2.Id, user3.Id);
				if (hashSet.Add(item))
				{
					list6.Add(new Follow
					{
						FollowerId = user2.Id,
						FollowedId = user3.Id
					});
				}
			}
		}
		modelBuilder.Entity<Follow>().HasData(list6.ToArray());
		List<Reaction> list7 = new List<Reaction>();
		for (int n = 1; n <= 30; n++)
		{
			int reactionType = random.Next(1, 4);
			list7.Add(new Reaction
			{
				Id = Guid.Parse($"50000000-0000-0000-0000-0000000000{n:D2}"),
				ReactionType = (ReactionType)reactionType,
				User = null
			});
		}
		modelBuilder.Entity<Reaction>().HasData(list7.ToArray());
		List<object> list8 = new List<object>();
		foreach (Post item2 in list4)
		{
			Tag tag = list3[random.Next(list3.Count)];
			list8.Add(new
			{
				PostsId = item2.Id,
				TagsId = tag.Id
			});
		}
		modelBuilder.Entity("PostTag").HasData(list8.ToArray());
	}
}
