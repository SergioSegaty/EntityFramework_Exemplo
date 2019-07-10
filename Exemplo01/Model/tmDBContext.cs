using System.Data.Entity;

namespace Exemplo01.Model
{
    class tmDBContext : DbContext
    {
        public DbSet<Status> Statuses { get; set; } 
        public DbSet<Task> Tasks { get; set; }
    }
}
