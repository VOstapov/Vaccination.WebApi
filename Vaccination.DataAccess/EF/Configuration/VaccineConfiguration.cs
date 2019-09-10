﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;

namespace Vaccination.DataAccess.EF.Configuration
{
    class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable("Vaccines");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Medication).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Agreement).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();

            builder.HasOne(x => x.Patient).WithMany(x => x.Vaccines).HasForeignKey(x => x.PatientId);

            builder.HasData(
                new Vaccine[]
                {
                    new Vaccine()
                    {
                        Id = 1,
                        PatientId = 1,
                        Medication = "Вианвак",
                        Date = new DateTime(2019, 05, 08),
                        Agreement = true
                    }
                });
        }
    }
}
