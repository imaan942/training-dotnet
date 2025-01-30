using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using fleetmanagement.config;
using fleetmanagement.repository;
using fleetmanagement.middleware;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5265");
// Read connection string from environment variable (for Docker support)
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

//Add database context with retry logic
builder.Services.AddDbContext<TruckDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure() // Enables retries if MySQL isn't ready
    )
);
// builder.Services.AddDbContext<TruckDbContext>(options => 
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8,0,39))
//         )
// );

// builder.Services.AddDbContext<DriverDbContext>(options => 
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8,0,39))
//         )
// );

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
// using (var scope = app.Services.CreateScope()){
//     var dbContext = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
//     // var dbContext2 = scope.ServiceProvider.GetRequiredService<DriverDbContext>();
//     // dbContext.Database.EnsureDeleted();
//     //dbContext.Database.Migrate();
//     // dbContext2.Database.EnsureDeleted();
//     // dbContext2.Database.EnsureCreated();
// }
using (var scope = app.Services.CreateScope()){
    var dbContext = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}
// Middleware for Exception Handling
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseFastEndpoints();

// Run the application
app.Run();

//setting up middleware
// app.UseMiddleware<GlobalExceptionMiddleware>();
// app.MapGet("/", () => "Hello World!");
// app.MapGet("/trucks", () => "Hello Truck!");;
