using Microsoft.EntityFrameworkCore;
using Cuidado.Models;

namespace Cuidado.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Applications> Applications { get; set; }
        public DbSet<Caregivers> Caregivers { get; set; }
        public DbSet<Elderly> Elderly { get; set; }
        public DbSet<Institutions> Instituitions { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== USERS ====================
            // User -> Caregiver (1:1)
            modelBuilder.Entity<Caregivers>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Caregivers>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Institution (1:1)
            modelBuilder.Entity<Institutions>()
                .HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<Institutions>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==================== REVIEWS ====================
            // Reviews -> FromUser (Restrict para evitar múltiplos caminhos)
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.FromUser)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews -> ToUser (Restrict)
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.ToUser)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews -> Shift (Restrict)
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Shift)
                .WithMany()
                .HasForeignKey(r => r.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==================== SHIFTS ====================
            // Shifts -> Institution (Restrict)
            modelBuilder.Entity<Shifts>()
                .HasOne(s => s.Institution)
                .WithMany(i => i.Shifts)
                .HasForeignKey(s => s.InstitutionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shifts -> Caregiver (Restrict, nullable)
            modelBuilder.Entity<Shifts>()
                .HasOne(s => s.Caregiver)
                .WithMany()
                .HasForeignKey(s => s.CaregiverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ==================== APPLICATIONS ====================
            // Applications -> Shift (Restrict)
            modelBuilder.Entity<Applications>()
                .HasOne(a => a.Shift)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Applications -> Caregiver (Restrict)
            modelBuilder.Entity<Applications>()
                .HasOne(a => a.Caregiver)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==================== ELDERLY ====================
            // Elderly -> Institution (Cascade: idoso pertence à institution)
            modelBuilder.Entity<Elderly>()
                .HasOne(e => e.Institution)
                .WithMany(i => i.Elderlies)
                .HasForeignKey(e => e.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
