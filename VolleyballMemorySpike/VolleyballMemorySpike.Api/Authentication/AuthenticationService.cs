using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VolleyballMemorySpike.Database.Entities.Users;

namespace VolleyballMemorySpike.Authentication;

public class AuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public AuthenticationService(
        IHttpContextAccessor httpContextAccessor, 
        IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _contextFactory = contextFactory;
    }

    public User? GetCurrentUser()
    {
        if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == false)
        {
            return null;
        }

        var userGuidFromAzure = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userNickName = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;
        var userEmail = _httpContextAccessor?.HttpContext?.User?.FindFirst("emails")?.Value;

        // TODO: Refactor and add domain execption
        if (string.IsNullOrEmpty(userGuidFromAzure) || 
            string.IsNullOrEmpty(userNickName) || 
            string.IsNullOrEmpty(userEmail))
        {
            throw new Exception();
        }

        var user = new User()
        {
            ObjectIdentifier = userGuidFromAzure,
            NickName = userNickName,
            Email = userEmail
        };


        return user;
    }

    public async Task<long?> GetCurrentUserId()
    {
        if(_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == false)
        {
            return null;
        }

        var userGuidFromAzure = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userGuidFromAzure))
        {
            return null;
        }

        using (var context = _contextFactory.CreateDbContext())
        {
            var userId = await context.Users
                .Where(user => string.Equals(user.ObjectIdentifier, userGuidFromAzure))
                .Select(user => user.Id)
                .SingleOrDefaultAsync();

            if (userId != null)
            {
                return userId;
            }
        }

        return null;
    }
}
