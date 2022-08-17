using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);
