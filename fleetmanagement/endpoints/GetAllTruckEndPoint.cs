using FastEndpoints;
using fleetmanagement.repository;
using fleetmanagement.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class GetAllTrucksEndPoint : EndpointWithoutRequest<IEnumerable<TruckResponse>>
{
    private readonly ITruckRepository _repository;

    public GetAllTrucksEndPoint(ITruckRepository repository)
    {
        _repository = repository; // Fixing the spelling of '_repository'
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/trucks");
        AllowAnonymous(); // Adding the missing semicolon
    }

    //sends the response back to the client.
    public override async Task HandleAsync(CancellationToken ct){
        var trucks = await _repository.GetAllTruckAsync();
        await SendAsync(trucks.Select(t => new TruckResponse{
            Id = t.Id,
            Name = t.Name,
            Model = t.Model,
            Year = t.Year
        }));
    }

}

