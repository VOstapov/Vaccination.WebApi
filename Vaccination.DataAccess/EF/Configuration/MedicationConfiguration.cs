using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;

namespace Vaccination.DataAccess.EF.Configuration
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.HasData(
                new Medication[]
                {
                    new Medication()
                    {
                        Id = 1,
                        Name = "Эджерикс"
                    },
                    new Medication()
                    {
                        Id = 2,
                        Name = "Вианвак"
                    },
                    new Medication()
                    {
                        Id = 3,
                        Name = "АКДС"
                    },
                    new Medication()
                    {
                        Id = 4,
                        Name = "БЦЖ"
                    }
                });
        }
    }
}
