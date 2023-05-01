using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ManageContacts.Service.CacheServices.RoleCaches;

public class RoleCacheService : IRoleCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public RoleCacheService(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }
    public async Task<IEnumerable<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var u = await _userRepository
            .GetAsync(predicate: u => u.UserId == userId, cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if (u == null)
            throw new NotFoundException("The user is not found");

        var cacheKey = $"user_roles_{userId}";
        if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<string> roles)) return roles;
        
        var urs = await _userRoleRepository
            .FindAllAsync(
                predicate: ur => ur.UserId == userId,
                include: r => r.Include(x => x.Role),
                cancellationToken: cancellationToken).ConfigureAwait(false);

        roles = urs.Select(ur => ur.Role.Name);

        var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));

        _memoryCache.Set(cacheKey, roles, cacheOptions);

        return roles;
    }
}