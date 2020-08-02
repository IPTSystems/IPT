namespace pms.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefDefinition>()
            .HasData(new { Id = 1.0M, RefName = "ACTIVITY", RefDataLength = 10, RefDataPrecision=0,RefDataType="STR" , RefDescription = "Activities supported by the operator", Version = 0 , CreateDT = DateTime.Now, LUDT = DateTime.Now });
        }
    }
}