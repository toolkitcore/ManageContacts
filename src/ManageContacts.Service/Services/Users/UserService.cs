using System.Text.Encodings.Web;
using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;
using ManageContacts.Service.AuthServices.AccessToken;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using ManageContacts.Shared.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManageContacts.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IRepository<User, ContactsContext> _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtSetting _jwtSetting;
    private readonly Guid _currentUserId;
    private readonly IWebHostEnvironment _env;
    
    public UserService(IRepository<User, ContactsContext> userRepository, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _jwtSetting = configuration.GetOptions<JwtSetting>() ?? throw new ArgumentNullException(nameof(configuration));
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _currentUserId = httpContextAccessor.GetCurrentUserId();
    }
    
    #region [ADMIN]
    public async Task<OkResponseModel<IPagedList<UserModel>>> GetAllAsync(UserFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var us = await _userRepository.PagingAllAsync(
                predicate: u => (string.IsNullOrEmpty(filter.SearchString) 
                                || u.FirstName.Contains(filter.SearchString)
                                || u.LastName.Contains(filter.SearchString)
                                || u.Email.Contains(filter.SearchString)
                                || u.PhoneNumber.Contains(filter.SearchString)) 
                                && u.Deleted == filter.Deleted,
                orderBy: u => u.OrderByDescending(x => x.CreatedTime),
                pageIndex: filter.PageIndex,
                pageSize: filter.PageSize,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        return new OkResponseModel<IPagedList<UserModel>>(_mapper.Map<PagedList<UserModel>>(us));
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
    #endregion

    #region [USER]
    public async Task<BaseResponseModel> SignUpAsync(UserRegistrationModel registerUser, CancellationToken cancellationToken = default)
    {
        var existUser = await _userRepository.GetAsync(
            predicate: u => u.UserName == registerUser.Username 
                            && u.Email == registerUser.Email 
                            && u.PhoneNumber == registerUser.PhoneNumber
                            && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (existUser != null)
            throw new BadRequestException("User with the same username, email or phone already exists.");
        
        var newUser = _mapper.Map<User>(registerUser);
        await _userRepository.InsertAsync(newUser, cancellationToken).ConfigureAwait(false);
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Successful account registration.");
    }

    public async Task<AuthorizedResponseModel> SignInAsync(UserLoginModel loginUser, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.UserName == loginUser.UserName && !u.Deleted,
            cancellationToken: cancellationToken
        );
        if (user == null)
            return new AuthorizedResponseModel("The email or password is invalid.");

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
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Chane password successfully");

    }

    public async Task<BaseResponseModel> UpdateProfileAsync(UserProfileEditModel userProfile, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Id == _currentUserId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new NotFoundException("The product is not found");
        
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
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

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