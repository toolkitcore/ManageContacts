using ManageContacts.Entity.Entities;
using ManageContacts.Entity.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Entity.Contexts;

public class ContactsContext : ApplicationDbContext
{
    public ContactsContext() { }
    
    public ContactsContext(DbContextOptions<ContactsContext> options) : base(options) { }

    // Phương thức OnConfiguring gọi mỗi khi một đối tượng DbContext được tạo
    // Nạp chồng nó để thiết lập các loại cấu hình, như thiết lập chuỗi kết nối
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
    
    // Phương thức này thi hành khi EnsureCreatedAsync chạy, tại đây gọi các Fluent API mong muốn 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRoleConfiguration).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactConfiguration).Assembly);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupConfiguration).Assembly);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyConfiguration).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneTypeConfiguration).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneNumberConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<PhoneType> PhoneTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<Company> Companies { get; set; }
    
}