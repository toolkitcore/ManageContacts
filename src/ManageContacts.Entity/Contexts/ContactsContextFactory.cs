using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManageContacts.Entity.Contexts;


/*
 * Khi chạy lệnh "dotnet ef" trong dự án của mình nó sẽ tìm kiếm  một lớp triển khai IDesignTimeDbContextFactory<TContext> trong mã nguồn
 * Nếu tìm thấy nó sẽ sử dụng lớp này để tạo ra một phiên bản "DbContext" tạm thời, từ đó nó sẽ xác định cơ sở dữ liệu và thực hiện các các tác vụ như tạo cơ sở dữ liệu, hoặc tạo đoạn mã cập nhập
 * Vì vậy, khi triển khai IDesignTimeDbContextFactory<TContext> và cung cấp mã để tạo ra một phiên bản DbContext, dotnet ef sẽ tìm thấy nó và sử dụng nó để thực hiện các tác vụ liên quan đến cơ sở dữ liệu
 */
public class ContactsContextFactory : IDesignTimeDbContextFactory<ContactsContext>
{
    public ContactsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContactsContext>();
        optionsBuilder.UseSqlServer(@"data source=G-32\SQLEXPRESS;initial catalog=ManageContacts;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True", opts =>
        {
            opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        });

        var context = new ContactsContext(optionsBuilder.Options);

        return context;
    }
}