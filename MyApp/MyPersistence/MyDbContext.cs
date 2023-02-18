using Microsoft.EntityFrameworkCore;

namespace MyPersistence
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }
    }
}
