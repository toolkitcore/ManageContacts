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

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}