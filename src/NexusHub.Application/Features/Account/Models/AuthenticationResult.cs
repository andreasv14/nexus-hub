namespace NexusHub.Application.Features.Account.Models;

public record AuthenticationResult(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Token);