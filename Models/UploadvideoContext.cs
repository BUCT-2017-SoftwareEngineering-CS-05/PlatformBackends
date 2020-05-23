using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPBackends.Models
{
    public class UploadvideoContext : DbContext
    {
        public DbSet<Uploadvideo> Uploadvideo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Uploadvideo>();
        }

        public UploadvideoContext(DbContextOptions<UploadvideoContext> options) : base(options)
        { }
    }
}
