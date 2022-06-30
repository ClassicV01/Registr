using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Registr.Models;

namespace Registr.Data
{
    public class RegistrContext : DbContext
    {
        public RegistrContext (DbContextOptions<RegistrContext> options)
            : base(options)
        {
        }

        public DbSet<Registr.Models.User>? User { get; set; }
    }
}
