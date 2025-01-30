// using FastEndpoints;
// using fleetmanagement.repository;
// using fleetmanagement.dto;
// using fleetmanagement.entity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;

// public class CreateTrucksEndPoint : Endpoint<TruckRequest, TruckResponse>
// {
//     private readonly ITruckRepository _repository;

    
//     public CreateTrucksEndPoint(ITruckRepository repository)
//     {
//         _repository = repository; // Fixing the spelling of '_repository'
//     }

//     public override void Configure()
//     {
//         Verbs(Http.POST);
//         Routes("/api/trucks");
//         AllowAnonymous(); // Adding the missing semicolon
//     }

//     //sends the response back to the client.
//     public override async Task HandleAsync(TruckRequest request, CancellationToken ct){
//         var truck = new Truck(request.Model, request.Name, request.Year);
//         var createdtruck = await _repository.AddTruckAsync(truck);

//         var response = new TruckResponse{
//             Id = createdtruck.Id,
//             Name = createdtruck.Name,
//             Model  = createdtruck.Model,
//             Year = createdtruck.Year
//         };
//         // Send the response back to the client
//         await SendAsync(response, cancellation: ct);
//     }
// }

using FastEndpoints;
using fleetmanagement.entity;
using fleetmanagement.dto;
using fleetmanagement.repository;

public class CreateTruckEndpoint: Endpoint<TruckRequest, TruckResponse>
{
    private readonly ITruckRepository _respository;

    public CreateTruckEndpoint(ITruckRepository repository){
        _respository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/trucks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(TruckRequest req, CancellationToken ct){
        
        var newTruck = new Truck(
            req.Name ?? string.Empty, 
            req.Model ?? string.Empty,
            req.Year
        );

        var createdTruck = await _respository.AddTruckAsync(newTruck);

        var response = new TruckResponse {
            Id = createdTruck.Id,
            Name = createdTruck.Name,
            Model = createdTruck.Model,
            Year = createdTruck.Year
        };
        await SendAsync(response);
    }
}