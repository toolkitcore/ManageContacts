using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
        modelBuilder.Entity<User>(m =>
        {
            m.HasQueryFilter(x => !x.Deleted);
            m.Property(u => u.CreatedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            m.Property(u => u.ModifiedTime).HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));
            m.HasOne(u => u.Creator).WithMany();
            m.HasOne(u => u.Modifier).WithMany();
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
}