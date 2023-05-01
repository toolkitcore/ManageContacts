using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;
using ManageContacts.Service.AuthServices.AccessToken;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using ManageContacts.Shared.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ManageContacts.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtSetting _jwtSetting;
    private readonly Guid _currentUserId;
    
    public UserService(IRepository<User> userRepository, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _jwtSetting = configuration.GetOptions<JwtSetting>() ?? throw new ArgumentNullException(nameof(configuration));
        _currentUserId = httpContextAccessor.GetCurrentUserId();
    }
    
    #region [ADMIN]
    public async Task<OkResponseModel<IPagedList<UserModel>>> GetAllAsync(UserFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var us = await _userRepository.PagingAllAsync(
                predicate: u => (!string.IsNullOrEmpty(filter.SearchString) 
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
                predicate: u => u.UserId == userId && u.Deleted,
                cancellationToken: cancellationToken
                ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<UserModel>(_mapper.Map<UserModel>(user));
    }
    

    public async Task<BaseResponseModel> DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.UserId == userId && u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");
        
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Deleted user successfully");
    }
    

    public async Task<BaseResponseModel> UndeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.UserId == userId && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (user == null)
            throw new BadRequestException("The request is invalid.");

        user.Deleted = true;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return new BaseResponseModel("Undelete user successfully.");
    }

    public async Task<BaseResponseModel> UndeleteUsersAsync(IEnumerable<Guid> listOfUserIds, CancellationToken cancellationToken)
    {
        if(!listOfUserIds.NotNullOrEmpty())
            throw new BadRequestException("The request is invalid.");
        
        var duplicate = listOfUserIds.HasDuplicated(i => i);
        if(duplicate)
            throw new BadRequestException("The list of UserIds contains duplicate values.");
        
        var users = await _userRepository.FindAllAsync(
            predicate: u => listOfUserIds.Contains(u.UserId) && !u.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if(!users.NotNullOrEmpty() || users.Count() != listOfUserIds.Count())
            throw new BadRequestException("The request is invalid.");
        
        foreach (var user in users)
        {
            user.Deleted = true;
        }

        await _userRepository.UpdateAsync(users.ToList(), cancellationToken).ConfigureAwait(false);
        await _userRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Undelete list user successfully.");
    }
    #endregion

    #region [USER]
    public async Task<BaseResponseModel> SignUpAsync(UserRegistrationModel registerUser, CancellationToken cancellationToken = default)
    {
        var existUser = await _userRepository.GetAsync(
            predicate: u => u.Email == registerUser.Email && u.PhoneNumber == registerUser.PhoneNumber,
            cancellationToken: cancellationToken
        );
        
        if (existUser != null)
            throw new BadRequestException("User with the same email or phone already exists.");
        
        var newUser = _mapper.Map<User>(registerUser);
        await _userRepository.InsertAsync(newUser, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new BaseResponseModel("Register ");
    }

    public async Task<AuthorizedResponseModel> SignInAsync(UserLoginModel loginUser, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(
            predicate: u => u.Email == loginUser.Email && u.Deleted,
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
        if (_currentUserId == Guid.Empty)
            throw new BadRequestException("The request is invalid.");
        
        var user = await _userRepository.GetAsync(
            predicate: u => u.UserId == _currentUserId && u.Deleted,
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
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> UpdateProfileAsync(UserProfileEditModel userProfile, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion
}