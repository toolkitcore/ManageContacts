using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;
using ManageContacts.Service.Abstractions.Core;
using ManageContacts.Service.AuthServices.AccessToken;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using ManageContacts.Shared.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ManageContacts.Service.Services.Users;

public class UserService : BaseService ,IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly JwtSetting _jwtSetting;
    private readonly IMemoryCache _memoryCache;

    public UserService(IUnitOfWork<ContactsContext> uow, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger,IWebHostEnvironment env, IMemoryCache memoryCache)
    : base(uow, httpContextAccessor, mapper, logger, env)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _jwtSetting = configuration.GetOptions<JwtSetting>() ?? throw new ArgumentNullException(nameof(configuration));
        _userRepository = uow.GetRepository<User>();
        _userRoleRepository = uow.GetRepository<UserRole>();
    }
    
    #region [ADMIN]
    public async Task<OkResponseModel<PaginationList<UserModel>>> GetAllAsync(UserFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.PagingAllAsync(
                predicate: u => 
                    (string.IsNullOrEmpty(filter.SearchString) 
                     || (!string.IsNullOrEmpty(filter.SearchString) && (u.FirstName.Contains(filter.SearchString) || u.LastName.Contains(filter.SearchString) || u.Email.Contains(filter.SearchString) || u.PhoneNumber.Contains(filter.SearchString)))) 
                    && u.Deleted == filter.Deleted,
                orderBy: u => u.OrderByDescending(x => x.CreatedTime),
                pageIndex: filter.PageIndex,
                pageSize: filter.PageSize,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (users.NotNullOrEmpty())
            return new OkResponseModel<PaginationList<UserModel>>(_mapper.Map<PaginationList<UserModel>>(users));

        return new OkResponseModel<PaginationList<UserModel>>(new PaginationList<UserModel>());
    }

    public async Task<OkResponseModel<UserModel>> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
                predicate: u => u.Id == userId && !u.Deleted,
                cancellationToken: cancellationToken
                ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<UserModel>(_mapper.Map<UserModel>(user));
    }

    public async Task<BaseResponseModel> CreateAsync(UserEditModel userEdit, CancellationToken cancellationToken = default)
    {
        var existUser = await _userRepository.GetAsync(
            predicate: u => (u.UserName == userEdit.UserName 
                            || u.Email == userEdit.Email 
                            || u.PhoneNumber == userEdit.PhoneNumber)
                            && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (existUser != null)
            throw new BadRequestException("User with the same username, email or phone already exists.");
        
        var targetPath = string.Empty;
        if (!string.IsNullOrEmpty(userEdit.Avatar))
            targetPath = Path.Combine(_env.WebRootPath, "images", "users", Path.GetFileName(userEdit.Avatar));
        
        var userNew = _mapper.Map<User>(userEdit);
        userNew.CreatorId = _currentUserId;
        userNew.Avatar = targetPath;

        await _userRepository.InsertAsync(userNew, cancellationToken).ConfigureAwait(false);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        if(!string.IsNullOrEmpty(targetPath))
            FileExtensions.MoveFile(userEdit.Avatar, targetPath);

        return new BaseResponseModel("Create user successful");
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid userId, UserEditModel userEdit, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        var existUser = await _userRepository.GetAsync(
            predicate: u => (u.UserName == userEdit.UserName 
                             || u.Email == userEdit.Email 
                             || u.PhoneNumber == userEdit.PhoneNumber)
                            && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (existUser != null)
            throw new BadRequestException("User with the same username, email or phone already exists.");
        
        var targetPath = string.Empty;
        if (!string.IsNullOrEmpty(userEdit.Avatar)) 
        {
            if (string.IsNullOrEmpty(user.Avatar))
            {
                targetPath = Path.Combine(_env.WebRootPath, "images", "users", Path.GetFileName(userEdit.Avatar));
            }
            else if (user.Avatar.Equals(userEdit.Avatar))
            {
                user.Avatar.DeleteFile();
                targetPath = Path.Combine(_env.WebRootPath, "images", "users", Path.GetFileName(userEdit.Avatar));
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar.DeleteFile();
            }
        }
        
        var userUpdate = _mapper.Map<User>(userEdit);
        userUpdate.ModifierId = _currentUserId;
        userUpdate.Avatar = targetPath;
        
        _userRepository.Update(userUpdate);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        _memoryCache.Remove($"user_roles_{userId}");
        
        if(!string.IsNullOrEmpty(targetPath))
            FileExtensions.MoveFile(userEdit.Avatar, targetPath);
        
        return new BaseResponseModel("Update user successful.");
    }

    public async Task<BaseResponseModel> DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        var urs = await _userRoleRepository.FindAllAsync(
            predicate: ur => ur.UserId == user.Id && !ur.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        _uow.BeginTransaction();
        _userRepository.Delete(user); 
        await _userRoleRepository.BulkDeleteAsync(urs.ToList(), cancellationToken).ConfigureAwait(false);
        await _uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        _memoryCache.Remove($"user_roles_{userId}");
        
        return new BaseResponseModel("Delete user successful.");
    }

    public async Task<BaseResponseModel> RecoverAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: r => r.Id == userId && r.Deleted,
            cancellationToken: cancellationToken,
            disableTracking: true
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");
        
        if(user.DeletedTime != null && (DateTime.UtcNow - user.DeletedTime.Value).Days > 30)
            throw new BadRequestException("Records deleted more than 30 days old cannot be recovered.");
        
        var urs = await _userRoleRepository.FindAllAsync(
            predicate: ur => ur.UserId == userId && ur.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        user.Deleted = false;
        foreach (var ur in urs)
        {
            ur.Deleted = false;
        }  
        
        _uow.BeginTransaction();
        _userRepository.Update(user);
        await _userRoleRepository.BulkUpdateAsync(urs.ToList(), cancellationToken).ConfigureAwait(false);
        await _uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        
        return new BaseResponseModel("Recover user successful");
    }

    #endregion

    #region [USER]
    public async Task<BaseResponseModel> SignUpAsync(UserRegistrationModel registerUser, CancellationToken cancellationToken = default)
    {
        var existUser = await _userRepository.GetAsync(
            predicate: u => (u.UserName == registerUser.UserName 
                             || u.Email == registerUser.Email 
                             || u.PhoneNumber == registerUser.PhoneNumber)
                            && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (existUser != null)
            throw new BadRequestException("User with the same username, email or phone already exists.");
        
        var newUser = _mapper.Map<User>(registerUser);
        await _userRepository.InsertAsync(newUser, cancellationToken).ConfigureAwait(false);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Successful account registration.");
    }

    public async Task<AuthorizedResponseModel> SignInAsync(UserLoginModel loginUser, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.UserName == loginUser.UserName && !u.Deleted,
            cancellationToken: cancellationToken
        );
        if (user == null)
            return new AuthorizedResponseModel("The username or password is invalid.");

        var passwordHashed = CryptoHelper.Encrypt(loginUser.Password, user.PasswordSalt);
        if (!passwordHashed.Equals(user.PasswordHashed))
            return new AuthorizedResponseModel("The username or password is invalid.");

        return AccessTokenService.GenerateJwtToken(user, _jwtSetting);
    }

    public async Task<AuthorizedResponseModel> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == _currentUserId && !u.Deleted,
            cancellationToken: cancellationToken
        );
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");
        
        return AccessTokenService.GenerateJwtToken(user, _jwtSetting);
    }

    public async Task<BaseResponseModel> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> ConfirmEmailAsync(Guid userId, string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<OkResponseModel<UserProfileModel>> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == _currentUserId && !u.Deleted,
            include: u => u.Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role),
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<UserProfileModel>(_mapper.Map<UserProfileModel>(user));
    }

    public async Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == _currentUserId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");
        
        var oldPasswordHashed = CryptoHelper.Encrypt(changePasswordModel.OldPassword, user.PasswordSalt);
        if(!oldPasswordHashed.Equals(user.PasswordHashed))
            throw new BadRequestException("Old password is incorrect.");
        
        user.PasswordHashed = CryptoHelper.Encrypt(changePasswordModel.NewPassword, user.PasswordSalt);

        _userRepository.Update(user);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Change password successfully");

    }

    public async Task<BaseResponseModel> UpdateProfileAsync(UserProfileEditModel userProfile, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == _currentUserId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new NotFoundException("The user is not found");
        
        var targetPath = string.Empty;
        if (!string.IsNullOrEmpty(userProfile.Avatar)) 
        {
            if (string.IsNullOrEmpty(user.Avatar))
            {
                targetPath = Path.Combine(_env.WebRootPath, "images", "users", Path.GetFileName(userProfile.Avatar));
            }
            else if (user.Avatar.Equals(userProfile.Avatar))
            {
                user.Avatar.DeleteFile();
                targetPath = Path.Combine(_env.WebRootPath, "images", "users", Path.GetFileName(userProfile.Avatar));
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar.DeleteFile();
            }
        }

        user.FirstName = userProfile.FirstName;
        user.LastName = userProfile.LastName;
        user.PhoneNumber = userProfile.PhoneNumber;
        user.Avatar = targetPath;

        _userRepository.Update(user);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if(!string.IsNullOrEmpty(targetPath))
            FileExtensions.MoveFile(userProfile.Avatar, targetPath);
        
        return new BaseResponseModel("Updated user successfully");
    }

    public async Task<bool> CheckExistUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        return user != null;
    }
    #endregion
}