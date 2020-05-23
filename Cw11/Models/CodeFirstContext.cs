using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class CodeFirstContext : DbContext
    {

        public DbSet<Patient> Patient { set; get; }
        public DbSet<Prescription> Prescription { set; get; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { set; get; }
        public DbSet<Medicament> Medicament { set; get; }
        public DbSet<Doctor> Doctor { set; get; }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity=> {
                entity.HasKey(e=>e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.IdPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();

                
            });

            modelBuilder.Entity<Prescription>(entity=>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Date).IsRequired();

                entity.HasOne(e => e.Patient).WithMany(p=>p.Prescriptions).HasForeignKey(d=>d.IdPatient).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Patient");
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(pm => pm.IdPresMed);
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                entity.Property(e => e.Dose).IsRequired(false);

                entity.HasOne(e => e.Prescription)
                      .WithMany(p => p.PrescriptionMedicaments)
                      .HasForeignKey(d => d.IdPrescription)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("PrescriptionMedicament_Prescriotion");

                entity.HasOne(e => e.Medicament)
                     .WithMany(p => p.PrescriptionMedicaments)
                     .HasForeignKey(d => d.IdMedicament)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("PrescriptionMedicament_Medicament");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();

            });
            modelBuilder.Entity<Doctor>(entity=> 
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });
        }
    }
}
