using ManageContacts.Entity.Entities;
using ManageContacts.Shared.Consts;
using ManageContacts.Shared.Helper;
using Serilog;

namespace ManageContacts.Entity.Contexts;


public static class ContactsContextSeed
{
    public static async Task SeedAsync(ContactsContext context, ILogger logger)
    {
        if (!context.Roles.Any() && !context.Users.Any())
        {
            var roles = GetRoles();
            await context.AddRangeAsync(roles.ToList()).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            var rs = context.Roles.ToList();
            var users = GetUsers(rs);
            await context.AddRangeAsync(users).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        if (!context.PhoneTypes.Any())
        {
            var phoneTypes = GetPhoneTypes();
            await context.AddRangeAsync(phoneTypes).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        
        
        logger.Information($"Seeded data for Contact Database associated with context {nameof(ContactsContext)}");

    }

    private static IEnumerable<Role> GetRoles()
    {
        return new List<Role>()
        {
            new()
            {
                Name = Roles.Administrator,
                Description = Roles.Administrator
            },
            new()
            {
                Name = Roles.Manager,
                Description = Roles.Manager
            },
            new()
            {
                Name = Roles.User,
                Description = Roles.User
            }
        };
    }

    private static IEnumerable<User> GetUsers(IEnumerable<Role> roles)
    {
        var passwordSaltAdmin = CryptoHelper.GenerateKey();
        var admin = new User()
        {
            Id = Guid.NewGuid(),
            UserName = "administrator",
            PasswordSalt = passwordSaltAdmin,
            PasswordHashed = CryptoHelper.Encrypt("administrator", passwordSaltAdmin),
            Email = "administrator.contacts@gmail.com",
            FirstName = "Administrator",
            LastName = "App",
            Avatar = String.Empty,
            PhoneNumber = "0969692765",
            UserRoles = new List<UserRole>()
            {
                new()
                {
                    RoleId = roles.First(x => x.Name == Roles.Administrator).Id
                }
            }
        };
        
        var passwordSaltManager = CryptoHelper.GenerateKey();
        var manager = new User()
        {
            Id = Guid.NewGuid(),
            UserName = "manager",
            PasswordSalt = passwordSaltAdmin,
            PasswordHashed = CryptoHelper.Encrypt("manager", passwordSaltAdmin),
            Email = "manager.contacts@gmail.com",
            FirstName = "Manager",
            LastName = "App",
            Avatar = String.Empty,
            PhoneNumber = "0976580418",
            UserRoles = new List<UserRole>()
            {
                new()
                {
                    RoleId = roles.First(x => x.Name == Roles.Manager).Id
                }
            }
        };

        var users = new List<User>()
        {

        };
        
        users.Add(admin);
        users.Add(manager);
        
        return users;


    }
    
    private static IEnumerable<PhoneType> GetPhoneTypes()
    {
        return new List<PhoneType>() 
        {
            new PhoneType() { TypeName = "Home", UnaccentedName = "home", Description = "Home." },
            new PhoneType() { TypeName = "Workplace", UnaccentedName = "workplace", Description = "Workplace." },
            new PhoneType() { TypeName = "Other", UnaccentedName = "other", Description = "Other." }
        };
    }
    
}