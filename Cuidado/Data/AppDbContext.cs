using Microsoft.EntityFrameworkCore;
using Cuidado.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cuidado.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Elderly> Elderlies { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== USERS ====================
            // User -> Caregiver (1:1)
            modelBuilder.Entity<Caregiver>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Caregiver>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Institution (1:1)
            modelBuilder.Entity<Institution>()
                .HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<Institution>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==================== REVIEWS ====================
            // Reviews -> FromUser (Restrict para evitar múltiplos caminhos)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.FromUser)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews -> ToUser (Restrict)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.ToUser)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews -> Shift (Restrict)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Shift)
                .WithMany()
                .HasForeignKey(r => r.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==================== SHIFTS ====================
            // Shifts -> Institution (Restrict)
            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Institution)
                .WithMany(i => i.Shifts)
                .HasForeignKey(s => s.InstitutionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shifts -> Caregiver (Restrict, nullable)
            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Caregiver)
                .WithMany()
                .HasForeignKey(s => s.CaregiverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ==================== APPLICATIONS ====================
            // Applications -> Shift (Restrict)
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Shift)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Applications -> Caregiver (Restrict)
            modelBuilder.Entity<Application>()
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
