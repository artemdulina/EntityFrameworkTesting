using System.Data.Entity;

namespace ORM
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {

        }

        public DbSet<Car> Tasks { get; set; }
    }
}
