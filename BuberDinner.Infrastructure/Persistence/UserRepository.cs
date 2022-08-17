using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static List<User> users = new();
    public void Add(User user)
    {
        users.Add(user);
    }

    public User? GetUserByEmail(string Email)
    {
        return users.FirstOrDefault(x => x.Email == Email);
    }
}