using AutoMapper;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace ManageContacts.Service.Abstractions.Core;

public class BaseService : IBaseService
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _uow;
    protected readonly ILogger _logger;
    protected readonly IWebHostEnvironment _env;
    protected readonly Guid _currentUserId;

    public BaseService(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger, IWebHostEnvironment env)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _env = env ?? throw new ArgumentNullException(nameof(env));

        _currentUserId = httpContextAccessor.GetCurrentUserId();
    }
    
}