using System.Security.Claims;
using KidsRUs.Application.Models.Dtos;

namespace KidsRUs.Application.Services;

public interface ITokenService
{
    TokenDto BuildToken(IEnumerable<Claim> claims, DateTime? expires = null);
    IEnumerable<Claim> GetClaims(string token);
}