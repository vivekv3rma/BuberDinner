using BuberDinner.Domain.Entities;
namespace BuberDinner.Application.Common.Intefaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string Email);
    void Add(User user);
}