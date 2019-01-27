using LibraryClass.Poco;
using Microsoft.EntityFrameworkCore;

namespace ApiDataTrainsFlow.Model
{
    public class Context:DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Analyze> Analyzes { get; set; }
        public Context(DbContextOptions<Context> options)
            : base(options)
        { }
    }
}
