using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Bear.Web.Entities {

    public class BearContext : DbContext {

        public BearContext() {
        }

        public BearContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Foo> Foos { get; set; }

    }

}
