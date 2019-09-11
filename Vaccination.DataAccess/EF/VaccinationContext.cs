using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.DataAccess.EF.Configuration;
using Vaccination.Domain.Models.Domain;

namespace Vaccination.DataAccess.EF
{
    public class VaccinationContext : DbContext
    {
        public DbSet<Patient> Contacts { get; set; }

        public VaccinationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new MedicationConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineConfiguration());
        }
    }
}
