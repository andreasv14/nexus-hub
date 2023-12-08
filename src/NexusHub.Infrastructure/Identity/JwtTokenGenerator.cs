using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexusHub.Application.Features.Account.Services;
using NexusHub.Application.Services;
using NexusHub.Infrastructure.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusHub.Infrastructure.Identity;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtOptions;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(
        IOptions<JwtSettings> jwtOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtOptions = jwtOptions.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, firstName + " " + lastName),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: _dateTimeProvider.Now.AddMonths(1),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}