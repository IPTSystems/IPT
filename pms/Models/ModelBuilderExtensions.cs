namespace pms.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefDefinition>()
            .HasData(new { Id = 1.0M, RefName = "ACTIVITY", RefDescription = "Activities supported by the operator", CreateDT = DateTime.Now, LUDT = DateTime.Now });
        }
    }
}