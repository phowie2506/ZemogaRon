using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZemogaRon.Models;

namespace ZemogaRon.Data
{
    public class ZemogaRonContext : DbContext
    {
        public ZemogaRonContext (DbContextOptions<ZemogaRonContext> options)
            : base(options)
        {
        }

        public DbSet<ZemogaRon.Models.Post> Post { get; set; }
        public DbSet<ZemogaRon.Models.Comment> Comment { get; set; }
    }
}
