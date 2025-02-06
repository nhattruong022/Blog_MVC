using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options) { }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CustomerModel> Customers {  get; set; }

    }
}
