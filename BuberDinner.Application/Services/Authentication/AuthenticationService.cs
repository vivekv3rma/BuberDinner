using BuberDinner.Application.Common.Intefaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string Email, string Password)
    {
        //1.Validate user exist
        if (_userRepository.GetUserByEmail(Email) is not User user)// == null)
        {
            throw new Exception("User doesn't exist for given email");
        }

        //2. Validate password
        if (user.Password != Password)
        {
            throw new Exception("Invalid Password");
        }

        //3. Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //1. Validate the user doesn't exist.
        if (_userRepository.GetUserByEmail(email) != null)
        {
            throw new Exception("User with given email already exist");
        }

        //2. Create a user(generate a unique id) and Persist to DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        //2. Create a JWT
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}