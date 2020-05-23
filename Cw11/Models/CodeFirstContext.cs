using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

                var tmp = new List<Patient>();
                tmp.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Alex", LastName = "Smith", IdPatient = 1 });
                tmp.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Brandon", LastName = "Johnson", IdPatient = 2 });
                tmp.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Bill", LastName = "Rodgers", IdPatient = 3 });
                entity.HasData(tmp);
            });

            modelBuilder.Entity<Prescription>(entity=>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.HasOne(e => e.Patient).WithMany(p=>p.Prescriptions).HasForeignKey(d=>d.IdPatient).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Patient");

                var tmp = new List<Prescription>();
                tmp.Add(new Prescription { IdPrescription = 1, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 1, IdPatient = 1 });
                tmp.Add(new Prescription { IdPrescription = 2, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 2, IdPatient = 2 });
                tmp.Add(new Prescription { IdPrescription = 3, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 3, IdPatient = 3 });
                entity.HasData(tmp)
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(pm => pm.IdPresMed);
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Dose).IsRequired(false);
                entity.HasOne(e => e.Prescription).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(d => d.IdPrescription).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PrescriptionMedicament_Prescriotion");
                entity.HasOne(e => e.Medicament).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(d => d.IdMedicament).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PrescriptionMedicament_Medicament");

                var tmp = new List<PrescriptionMedicament>();
                tmp.Add(new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Details = "some details1", Dose = 1 });
                tmp.Add(new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Details = "some details2", Dose = 2 });
                tmp.Add(new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 3, Details = "some details3", Dose = 3 });
                entity.HasData(tmp);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();

                var tmp = new List<Medicament>();
                tmp.Add(new Medicament { IdMedicament = 1, Name = "Peropholin", Description = "No descriptin name is not real", Type = "Placebo" });
                tmp.Add(new Medicament { IdMedicament = 2, Name = "Asperine", Description = "Against headache", Type = "Regular pils" });
                tmp.Add(new Medicament { IdMedicament = 3, Name = "Dekasan", Description = "For patients with lung's desease", Type = "Anti-biotic" });
                entity.HasData(tmp);
            });
            modelBuilder.Entity<Doctor>(entity=> 
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

                var tmp = new List<Doctor>();
                tmp.Add(new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Smith", Email = "john@mail" });
                tmp.Add(new Doctor { IdDoctor = 2, FirstName = "Sarah", LastName = "Jackson", Email = "sarah@mail" });
                tmp.Add(new Doctor { IdDoctor = 3, FirstName = "Tony", LastName = "Brown", Email = "tony@mail" });
                entity.HasData(tmp);
            });
        }
    }
}
