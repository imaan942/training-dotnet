using Microsoft.EntityFrameworkCore;
using fleetmanagement.entity;

namespace fleetmanagement.config;

//acts like a bridge between app and database
//provides methods to query methods
public class DriverDbContext : DbContext{
    public DriverDbContext( DbContextOptions<DriverDbContext> options):base(options){}

    //collection of entities
    public DbSet<Driver> Drivers {get; set;} //ORM

    //method to configure shape of entities
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);
        base.OnModelCreating(modelBuilder);
    }

}
