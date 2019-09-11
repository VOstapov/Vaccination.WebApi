using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;

namespace Vaccination.DataAccess.EF.Configuration
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.HasData(
                new Gender[]
                {
                    new Gender()
                    {
                        Id = 1,
                        Name = "Мужской"
                    },
                    new Gender()
                    {
                        Id = 2,
                        Name = "Женский"
                    }
                });
        }
    }
}
