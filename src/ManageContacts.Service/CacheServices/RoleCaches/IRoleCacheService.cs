namespace ManageContacts.Service.CacheServices.RoleCaches;

public interface IRoleCacheService
{
    Task<IEnumerable<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default);
}