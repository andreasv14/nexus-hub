using NexusHub.Application.Features.Account.Models;
using NexusHub.Application.Features.Account.Services;

namespace NexusHub.Application.Features.Account.Commands;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResult>;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
    }
}

public class RegisterUserCommand : IRequestHandler<RegisterUserRequest, AuthenticationResult>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommand(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        return await _identityService.RegisterAsync(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
    }
}
