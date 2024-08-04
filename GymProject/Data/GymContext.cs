using GymProject.DataModelEntities;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data;

public class GymContext(DbContextOptions<GymContext> options) : DbContext(options)
{
    //Representation of objects that need to be mapped to the database

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Reason> Reasons => Set<Reason>();

    //Method executed upon migration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reason>().HasData(
            new Reason { Id = 1, NameOfReason = "Lose Weight" },
            new Reason { Id = 2, NameOfReason = "Gain Muscle" },
            new Reason { Id = 3, NameOfReason = "Maintain Weight" },
            new Reason { Id = 4, NameOfReason = "Improve Cardio" },
            new Reason { Id = 5, NameOfReason = "Improve Flexibility" }
        );
    }
}
