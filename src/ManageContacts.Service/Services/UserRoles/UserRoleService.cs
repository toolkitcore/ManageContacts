using AutoMapper;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.UserRoles;
using ManageContacts.Service.Abstractions.Core;
using ManageContacts.Shared.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ManageContacts.Service.Services.UserRoles;

public class UserRoleService : BaseService ,IUserRoleService
{
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IRepository<User> _userRepository;
    public UserRoleService(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger, IWebHostEnvironment env)
        : base(uow, httpContextAccessor, mapper, logger, env)
    {
        _userRoleRepository = uow.GetRepository<UserRole>();
        _userRepository = uow.GetRepository<User>();
    }

    public async Task<BaseResponseModel> AddOrUpdateUserRoleAsync(Guid userId, UserRoleEditModel userRoleEdit,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The user is not found.");

        
        
        return default!;
    }

    public async Task<BaseResponseModel> RemoveUserRoleAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The user is not found.");
        
        var urs = await _userRoleRepository
            .FindAllAsync(
                predicate: ur => ur.UserId == userId,
                cancellationToken: cancellationToken).ConfigureAwait(false);

        await _userRoleRepository.BulkDeleteAsync(urs.ToList(), cancellationToken).ConfigureAwait(false);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Remove user role successful.");
    }
}