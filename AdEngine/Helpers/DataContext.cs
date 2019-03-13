using AdEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AdEngine.API.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<UserModel> Users {get; set;}
    }
}
