using System;
using System.Collections.Generic;

namespace Entities.Entities;

public class Category
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public ICollection<Post> Posts { get; set; } = new List<Post>();
}
