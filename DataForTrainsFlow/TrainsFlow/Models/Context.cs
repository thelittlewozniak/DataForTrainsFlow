using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainsFlow.Models
{
    public class Context : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Data> Datas { get; set; }
        public Context(DbContextOptions<Context> options)
            : base(options)
        { }
    }
}
