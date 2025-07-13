using Entities;
using ServicesContracts.DTOs;

namespace ServicesContracts.Mappers;

public static class UserMapper
{
	public static User ToEntity(UserDTO user)
	{
		return new User
		{
			Name = user.Name,
			UserName = user.UserName,
			Email = user.Email,
			ProfilePhoto = (user.ProfilePhoto ?? string.Empty),
			About = (user.About ?? string.Empty)
		};
	}

	public static UserDTO ToDTO(User user)
	{
		return new UserDTO
		{
			Name = user.Name,
			UserName = user.UserName,
			Email = user.Email,
			ProfilePhoto = (user.ProfilePhoto ?? string.Empty),
			About = (user.About ?? string.Empty)
		};
	}
}
