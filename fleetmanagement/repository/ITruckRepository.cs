using fleetmanagement.entity;

namespace fleetmanagement.repository;

public interface ITruckRepository{
    Task<IEnumerable<Truck>> GetAllTruckAsync();
    Task<Truck> GetTruckByIdAsync(int id);
    Task<Truck> AddTruckAsync(Truck truck);
    Task<Truck> UpdateTruckAsync(Truck truck);
    Task<bool> DeleteTruckAsync(int id);
};
