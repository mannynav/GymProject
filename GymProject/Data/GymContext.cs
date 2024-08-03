using GymProject.DataModelEntities;
using GymProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data;

public class GymContext(DbContextOptions<GymContext> options) : DbContext(options)
{
    //Representation of objects that need to be mapped to the database

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Aims> Aims => Set<Aims>();

    //Method executed upon migration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}
