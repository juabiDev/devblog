using System;
using Entities.Enum;

namespace Entities;

public class Reaction
{
	public Guid Id { get; set; }

	public ReactionType ReactionType { get; set; }

	public User? User { get; set; }
}
