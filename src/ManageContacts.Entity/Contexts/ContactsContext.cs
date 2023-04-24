using ManageContacts.Entity.Contacts;
using ManageContacts.Entity.Groups;
using ManageContacts.Entity.Roles;
using ManageContacts.Entity.UserRoles;
using ManageContacts.Entity.Users;
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
        #region [Users]
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasQueryFilter(u => !u.Deleted);
            builder.Property(u => u.CreatedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(u => u.ModifiedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));
            builder.HasOne(u => u.Creator).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Modifier).WithMany().OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region [Roles]
        modelBuilder.Entity<Role>(builder =>
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.Property(u => u.CreatedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(u => u.ModifiedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));
            builder.HasOne(u => u.Creator).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Modifier).WithMany().OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region UserRole
        modelBuilder.Entity<UserRole>(builder =>
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            
            builder.HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.UserId);
            
            builder.HasOne<Role>(sc => sc.Role)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.RoleId);
            
            builder.HasOne(u => u.Creator).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.User).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Role).WithOne().OnDelete(DeleteBehavior.Cascade);
        });
        
        #endregion
        

        #region [Contacts]
        modelBuilder.Entity<Contact>(builder =>
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.Property(u => u.CreatedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(u => u.ModifiedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));
            builder.HasOne(u => u.Creator).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Modifier).WithMany().OnDelete(DeleteBehavior.NoAction);
        });

        #endregion

        #region [Group Contacts]
        modelBuilder.Entity<Group>(builder =>
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.Property(u => u.CreatedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(u => u.ModifiedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));
            builder.HasOne(u => u.Creator).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Modifier).WithMany().OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region [Contact Phones]
        
        #endregion

        #region [Phone Types]
        
        #endregion
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}