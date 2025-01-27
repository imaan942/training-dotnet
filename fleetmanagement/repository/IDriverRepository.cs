using Microsoft.EntityFrameworkCore;
using fleetmanagement.entity;
using fleetmanagement.config;

namespace fleetmanagement.repository;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllDriversAsync();
}