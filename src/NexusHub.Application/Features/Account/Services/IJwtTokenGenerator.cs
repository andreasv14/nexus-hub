namespace NexusHub.Application.Features.Account.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}
