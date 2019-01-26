using LibraryClass.Poco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeatherTrainsFlow.Model
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
