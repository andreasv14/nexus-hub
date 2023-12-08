using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusHub.Application.Features.Account.Commands;

namespace NexusHub.WebAPI.Controllers;

/// <summary>
/// Authentication endpoints
/// </summary>
[AllowAnonymous]
public class AuthController : ApiControllerBase
{
    /// <summary>
    /// Register as a new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

    /// <summary>
    /// Login as an existing user using email and password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}