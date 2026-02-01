using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Domain.Entities.Certificates;
using ResumeMaker.Domain.Entities.Educations;
using ResumeMaker.Domain.Entities.Jobs;
using ResumeMaker.Domain.Entities.Projects;
using ResumeMaker.Domain.Entities.Skills;
using ResumeMaker.Domain.Entities.Volunteers;

namespace ResumeMaker.Persistence.Context;

public class ResumeMakerDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ResumeMakerDbContext(DbContextOptions<ResumeMakerDbContext> options) : base(options)
    {
    }

    public DbSet<Resume> Resumes { get; set; }
    public DbSet<SkillCategory> SkillCategories { get; set; }
    public DbSet<TechnologyHub> TechnologyHubs { get; set; }
    public DbSet<JobsHistory> JobsHistories { get; set; }
    public DbSet<JobDescription> JobDescriptions { get; set; }
    public DbSet<EducationInfo> EducationInfos { get; set; }
    public DbSet<ProjectsInfo> ProjectsInfos { get; set; }
    public DbSet<ProjectDefinitions> ProjectDefinitions { get; set; }
    public DbSet<VolunteerInfo> VolunteerInfos { get; set; }
    public DbSet<VolunteerDescription> VolunteerDescriptions { get; set; }
    public DbSet<CertificatesInfo> CertificatesInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<SkillCategory>()
            .HasOne(sc => sc.Resume)
            .WithMany(r => r.SkillCategories)
            .HasForeignKey(sc => sc.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TechnologyHub>()
            .HasOne(t => t.SkillCategory)    
            .WithMany(c => c.TechnologyHubs) 
            .HasForeignKey(t => t.SkillCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<JobsHistory>()
            .HasOne(sc => sc.Resume)
            .WithMany(r => r.JobsHistories)
            .HasForeignKey(sc => sc.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<JobDescription>()
            .HasOne(sc => sc.JobsHistory)
            .WithMany(r => r.JobDescriptions)
            .HasForeignKey(sc => sc.JobsHistoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<VolunteerInfo>()
            .HasOne(sc => sc.Resume)
            .WithMany(r => r.VolunteerInfos)
            .HasForeignKey(sc => sc.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<VolunteerDescription>()
            .HasOne(sc => sc.VolunteerInfo)
            .WithMany(r => r.VolunteerDescriptions)
            .HasForeignKey(sc => sc.VolunteerInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ProjectsInfo>()
            .HasOne(sc => sc.Resume)
            .WithMany(r => r.ProjectsInfos)
            .HasForeignKey(sc => sc.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ProjectDefinitions>()
            .HasOne(sc => sc.ProjectsInfo)
            .WithMany(r => r.ProjectDefinitions)
            .HasForeignKey(sc => sc.ProjectsInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EducationInfo>()
            .HasOne(e => e.Resume)
            .WithMany(r => r.EducationInfos)
            .HasForeignKey(e => e.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CertificatesInfo>()
            .HasOne(c => c.Resume)
            .WithMany(r => r.CertificatesInfos)
            .HasForeignKey(c => c.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
