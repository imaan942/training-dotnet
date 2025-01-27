// using FastEndpoints;
// using fleetmanagement.repository;
// using fleetmanagement.dto;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;

// public class CreateTruckEndPoint : EndpointWithoutRequest<IEnumerable<TruckResponse>>
// {
//     private readonly ITruckRepository _repository;

//     public CreateTruckEndPoint(ITruckRepository repository)
//     {
//         _repository = repository; // Fixing the spelling of '_repository'
//     }

//     public override void Configure()
//     {
//         Verbs(Http.POST);
//         Routes("/api/trucks");
//         AllowAnonymous(); // Optional, depends on your authorization setup
//     }

//     // Handles creating a new truck
//     public override async Task HandleAsync(CancellationToken ct)
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
// }
