using NexusHub.Application.Features.Account.Models;
using NexusHub.Application.Features.Account.Services;

namespace NexusHub.Application.Features.Account.Commands;

public record LoginRequest(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}

public class LoginCommand : IRequestHandler<LoginRequest, AuthenticationResult>
{
    private readonly IIdentityService _identityService;

    public LoginCommand(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthenticationResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(request.Email, request.Password);
    }
}