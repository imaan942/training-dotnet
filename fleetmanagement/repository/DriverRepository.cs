using Microsoft.EntityFrameworkCore;
using fleetmanagement.entity;
using fleetmanagement.config;

namespace fleetmanagement.repository;

public class DriverRepository : IDriverRepository
{
    private readonly DbContext _context;

    public DriverRepository(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetAllDriversAsync()
    {
        return await _context.Set<Driver>().ToListAsync();
    }
}