using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Reflection.Metadata;

namespace JobHosting.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobListing>()
                .ToTable(tb => tb.UseSqlOutputClause(false));
        }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<JobLister> JobListers { get; set; }
    }
}
