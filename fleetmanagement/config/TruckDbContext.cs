using Microsoft.EntityFrameworkCore;
using fleetmanagement.entity;

namespace fleetmanagement.config;

//acts like a bridge between app and database
//provides methods to query methods
public class TruckDbContext : DbContext{
    public TruckDbContext( DbContextOptions<TruckDbContext> options):base(options){}

    //collection of entities
    public DbSet<Truck> Trucks {get; set;} //ORM
    // public DbSet<Driver> Drivers {get; set;} //ORM

    //method to configure shape of entities
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Truck>().HasKey(t => t.Id);
        // modelBuilder.Entity<Driver>().HasKey(d => d.Id);
        base.OnModelCreating(modelBuilder);
    }

}
