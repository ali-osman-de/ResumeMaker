using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Domain.Entities.Resume;

namespace ResumeMaker.Persistence.Context
{
    public class ResumeMakerDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ResumeMakerDbContext(DbContextOptions<ResumeMakerDbContext> options) : base(options)
        {
        }

        public DbSet<ResumeSum> Resumes { get; set; }
        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<Summary> Summaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ResumeSum>(entity =>
            {
                entity.HasOne(r => r.PersonalDetail)
                      .WithOne(pd => pd.ResumeSum)
                      .HasForeignKey<PersonalDetail>(pd => pd.ResumeSumId);

                entity.HasOne(r => r.Summary)
                      .WithOne(s => s.ResumeSum)
                      .HasForeignKey<Summary>(s => s.ResumeSumId);

                entity.HasOne(r => r.AppUser)
                      .WithMany(u => u.ResumeSums)
                      .HasForeignKey(r => r.AppUserId);
            });
        }
    }
}
