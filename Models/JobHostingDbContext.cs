using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Reflection.Metadata;
using jobHosting.Models;

namespace JobHosting.Models
{
    public class JobHostingDbContext: IdentityDbContext<UserAccount>
    {
        public JobHostingDbContext(DbContextOptions<JobHostingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<JobListing>()
                // source
                .HasOne(listing => listing.JobsLister)
                // target
                .WithMany(user => user.Listings)
                // foreign key
                .HasForeignKey(listing => listing.JobsListerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
