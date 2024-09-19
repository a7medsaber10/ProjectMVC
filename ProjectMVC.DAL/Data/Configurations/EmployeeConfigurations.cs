using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary)
                .HasColumnType("decimal(18,2)");

            builder.Property(E => E.Gender)
                .HasConversion(
                    (Gender) => Gender.ToString(), // Label of the member to represent it in DB
                    (GenderAsString) => (Gender) Enum.Parse(typeof(Gender),GenderAsString)
                );

            builder.Property(E => E.Name)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
