// using FastEndpoints;
// using fleetmanagement.repository;
// using fleetmanagement.dto;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using fleetmanagement.entity;
// using System.Linq.Expressions;
using FastEndpoints;
using fleetmanagement.dto;
using fleetmanagement.entity;
using fleetmanagement.repository;

public class UpdateTruckEndpoint : Endpoint<TruckRequest, TruckResponse>
{
    private readonly ITruckRepository _truckRepository;

    public UpdateTruckEndpoint(ITruckRepository repository)
    {
        _truckRepository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/api/trucks/{id:int}");
        DontCatchExceptions(); // Exceptions will be passed to the middleware
        AllowAnonymous();
    }

public override async Task HandleAsync(TruckRequest req, CancellationToken ct)
{
    var id = Route<int>("id");
    var existingTruck = await _truckRepository.GetTruckByIdAsync(id);

    if (existingTruck == null)
    {
        await SendNotFoundAsync();
        return;
    }

    if (req.Year != 0 && (req.Year < 1900 || req.Year > DateTime.Now.Year))
    {
        throw new ValidationErrorFaulureException(new[]
        {
            new ValidationErrorDetail("Year", $"Year should be between 1900 and {DateTime.Now.Year}.")
        });
    }

    existingTruck.Name = req.Name ?? existingTruck.Name;
    existingTruck.Model = req.Model ?? existingTruck.Model;
    existingTruck.Year = req.Year != 0 ? req.Year : existingTruck.Year;

    var updatedTruck = await _truckRepository.UpdateTruckAsync(existingTruck);

    await SendAsync(new TruckResponse
    {
        Id = updatedTruck.Id,
        Name = updatedTruck.Name,
        Model = updatedTruck.Model,
        Year = updatedTruck.Year
       // Message = "Truck updated successfully!"
    });
}


}


// public class UpdateTruckEndPoint: Endpoint<TruckRequest, TruckResponse>{

// //     private readonly ITruckRepository _repository;

// //     public UpdateTruckEndPoint(ITruckRepository repository)
// //     {
// //         _repository = repository; // Fixing the spelling of '_repository'
// //     }
// //     public override async Task HandleAsync(TruckRequest req, CancellationToken ct){
// //         var existingTruck = _repository.GetTruckByIdAsync(req.Id);
// //         var updatedtruck = await _repositroy.UpdateTruckAsync(existingTruck);
// //         var response = new TruckResponse{
// //             Id = updatedtruck.Id,
// //             Name = updatedtruck.Name,
// //             Model  = updatedtruck.Model,
// //             Year = updatedtruck.Year
// //         };
// //         catch (Exception ex)
// //         {
// //             var 
// //         }
// //     }
// // }
// // public class UpdateTruckEndPoint : EndpointWithoutRequest<IEnumerable<TruckResponse>>
// // {
//     private readonly ITruckRepository _repository;

//     public UpdateTruckEndPoint(ITruckRepository repository)
//     {
//         _repository = repository; // Fixing the spelling of '_repository'
//     }

//     public override void Configure()
//     {
//         Verbs(Http.POST);
//         Routes("/api/trucks/{id}");
//         AllowAnonymous(); // Optional, depends on your authorization setup
//     }

//     // Handles creating a new truck
//     public override async Task HandleAsync( TruckRequest request, CancellationToken ct)
//     {
//         // Create a new truck entity based on the incoming request
//         var truck = new Truck
//         {
//             Name = request.Name,
//             Model = request.Model,
//             Year = request.Year
//         };

//         // Save the truck to the repository
//         var createdTruck = await _repository.UpdateTruckAsync(truck);

//         // Send the response back to the client with the created truck data
//         await SendAsync(new TruckResponse
//         {
//             Id = createdTruck.Id,
//             Name = createdTruck.Name,
//             Model = createdTruck.Model,
//             Year = createdTruck.Year
//         });
//     }
//     }