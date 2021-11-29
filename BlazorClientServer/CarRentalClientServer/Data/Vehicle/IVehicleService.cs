﻿using CarRentalClientServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalClientServer.Data
{
    public interface IVehicleService
    {

        //rename to vehicle
        Task<IList<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleAsync(int id);
        Task<Vehicle> CreateVehicleAsync(string name, string type, int pricePerDay, int seatsCount, bool isAutomatic,
            int powerKw, string fuelType, int deposit);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);
        
    }
}