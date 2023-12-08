using NexusHub.Application.Features.Account.Models;

namespace NexusHub.Application.Features.Account.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password);

    Task<AuthenticationResult> LoginAsync(string email, string password);
}