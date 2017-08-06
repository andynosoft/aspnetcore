using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySQL.Data.Entity.Extensions;

namespace Bear.Web.Entities {

    public class BearContextFactory : IDbContextFactory<BearContext> {

        public BearContext Create(DbContextFactoryOptions options) {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(options.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{options.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var config = builder.Build();

            var connectionString = config.GetConnectionString("BearContext");

            var optionsBuilder = new DbContextOptionsBuilder<BearContext>();
            optionsBuilder.UseMySQL(connectionString);

            return new BearContext(optionsBuilder.Options);

        }

    }

}
