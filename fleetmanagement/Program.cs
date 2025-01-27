using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using fleetmanagement.config;
using fleetmanagement.repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TruckDbContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8,0,39))
        )
);

builder.Services.AddDbContext<DriverDbContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8,0,39))
        )
);

// builder.Services.AddDbContext<DriverDbContext>(options => 
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8, 0, 39))
//     )
// );

builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddFastEndpoints();
var app = builder.Build();

//Ensure the database and tables are created automatically (for dev env)
using (var scope = app.Services.CreateScope()){
    var dbContext = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope()){
    var dbContext = scope.ServiceProvider.GetRequiredService<DriverDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

//setting up middleware
app.UseFastEndpoints();
// app.MapGet("/", () => "Hello World!");
// app.MapGet("/trucks", () => "Hello Truck!");
app.Run();
