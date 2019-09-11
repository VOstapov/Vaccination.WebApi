using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;

namespace Vaccination.DataAccess.EF.Configuration
{
    class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Soname).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Patronomic).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.GenderId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Snils).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Gender).WithMany(x => x.Patients).HasForeignKey(x => x.GenderId);

            builder.HasData(
                new Patient[]
                {
                    new Patient()
                    {
                        Id = 1,
                        Name = "Борис",
                        Soname = "Ганичев",
                        Patronomic = "Петрович",
                        GenderId = 1,
                        Birthday = new DateTime(1985,1,5),
                        Snils = "112-233-445 95"
                    }
                });
        }
    }
}
