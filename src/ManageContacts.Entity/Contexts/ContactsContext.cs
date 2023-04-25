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
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.ApplyConfiguration(new RoleConfiguration());

        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        
        modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new EmailTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new PhoneTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new RelativeTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new ContactAddressConfiguration());
        
        modelBuilder.ApplyConfiguration(new ContactEmailConfiguration());
        
        modelBuilder.ApplyConfiguration(new ContactPhoneConfiguration());
        
        modelBuilder.ApplyConfiguration(new ContactRelativeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<PhoneType> PhoneTypes { get; set; }
    public DbSet<EmailType> EmailTypes { get; set; }
    public DbSet<RelativeType> RelativeTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactAddress> ContactAddresses { get; set; }
    public DbSet<ContactEmail> ContactEmails { get; set; }
    public DbSet<ContactPhone> ContactPhones { get; set; }
    public DbSet<ContactRelative> ContactRelatives { get; set; }
    public DbSet<Company> Companies { get; set; }

    
}