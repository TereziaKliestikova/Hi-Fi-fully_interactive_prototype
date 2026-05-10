using Microsoft.EntityFrameworkCore;
using HIPA_BE.Data.Seeding;
using HIPA_BE.Models.Admin.FlagModels;
// using HIPA_BE.Data.Seeding.Admin;
using Directory = HIPA_BE.Models.LearningModels.Directory;

namespace HIPA_BE.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Models.BodySystemModels.BodySystem> BodySystems { get; set; }
        public DbSet<Models.OrganModels.Organ> Organs { get; set; }
        public DbSet<Models.Diagnosis> Diagnoses { get; set; }
        public DbSet<Models.SampleImage> SampleImages { get; set; }
        public DbSet<Models.SampleImageModels.SampleImageNote> SampleImageNotes { get; set; }
        public DbSet<Models.SampleImageAnnotationModels.SampleImageAnnotation> SampleImageAnnotations { get; set; }
        public DbSet<Models.FavoriteSample> FavoriteSamples { get; set; }
        public DbSet<Models.PdfFileModels.PdfFile> PdfFiles { get; set; }
        public DbSet<FlagType> FlagTypes { get; set; }
        public DbSet<SampleImageFlag> SampleImageFlags { get; set; }

        public DbSet<Directory> Directories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.BodySystemModels.BodySystem>()
            .HasMany(b => b.Organs)
            .WithMany(o => o.BodySystems)
            .UsingEntity(j => j.ToTable("BodySystemOrgan"));

            modelBuilder.Entity<Models.BodySystemModels.BodySystem>()
                .HasMany(b => b.Pdfs)
                .WithOne(p => p.BodySystem)
                .HasForeignKey("BodySystemID")      
                .OnDelete(DeleteBehavior.Cascade);  


            modelBuilder.Entity<Models.OrganModels.Organ>()
            .HasMany(o => o.Pdfs)
            .WithOne(p => p.Organ);

            modelBuilder.Entity<Directory>()
                .HasMany(d => d.SampleImages)
                .WithMany(s => s.ParentDirectories);
            
            modelBuilder.Entity<Directory>()
                .HasMany(d => d.ChildDirectories)
                .WithOne(d => d.Parent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Directory>()
                .HasMany(d => d.Files)
                .WithOne(f => f.Directory)
                .HasForeignKey(f => f.DirectoryId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var organs = OrganSeeding.GetModels();
            var diagnoses = DiagnosisSeeding.GetModels();

            modelBuilder.Entity<Models.BodySystemModels.BodySystem>().HasData(
                BodySystemSeeding.GetModels()
            );
            modelBuilder.Entity<Models.OrganModels.Organ>().HasData(
                OrganSeeding.GetModels()
            );

            modelBuilder.Entity<Models.Diagnosis>().HasData(
                diagnoses
            );

            modelBuilder.Entity("BodySystemOrgan").HasData(
                BodySystemOrganSeeding.GetModels()
            );

            modelBuilder.Entity<Models.PdfFileModels.PdfFile>().HasData(
                 PdfFileSeeding.GetModels()
            );

            modelBuilder.Entity<Models.SampleImage>().HasData(
                SampleImageSeeding.GetModels(organs.Count, diagnoses.Count)
            );

            modelBuilder.Entity<Models.SampleImageAnnotationModels.SampleImageAnnotation>().HasData(
                SampleImageAnnotationSeeding.GetModels(organs.Count)
            );

            // modelBuilder.Entity<FlagType>().HasData(
            //     FlagTypeSeeding.GetModels()
            // );
        }

    }
}