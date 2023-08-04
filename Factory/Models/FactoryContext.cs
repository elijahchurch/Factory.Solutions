using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
    public class FactoryContext : DbContext
    {
        public DBSet<Engineer> Engineers { get; set; }
        public DBSet<Macine> Machines { get; set; }
        public DBSet<EngineerMacine> EngineerMachines { get; set; }
        public FactoryContext(DbContextOptions options) : base(options) { }
    }
}