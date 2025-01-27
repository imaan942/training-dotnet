using FastEndpoints;
using fleetmanagement.repository;
using fleetmanagement.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class GetAllDriversEndPoint : EndpointWithoutRequest<IEnumerable<DriverResponse>>
{
    private readonly IDriverRepository _repository;

    public GetAllDriversEndPoint(IDriverRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/drivers");
        AllowAnonymous(); // or you can apply authentication and authorization
    }

    // Fetch all drivers and return as DriverResponse
    public override async Task HandleAsync(CancellationToken ct)
    {
        var drivers = await _repository.GetAllDriversAsync();
        var driverResponses = drivers.Select(d => new DriverResponse
        {
            Id = d.Id,
            Name = d.Name,
            License = d.License,
            Details = d.Details
        });

        await SendAsync(driverResponses);
    }
}
