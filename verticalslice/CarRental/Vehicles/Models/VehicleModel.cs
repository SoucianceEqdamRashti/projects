using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Vehicles.Models;
public class GetAllVehiclesCommandResult
{
    public GetAllVehiclesCommandResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;
        
    }

    public GetAllVehiclesCommandResult(List<AllVehiclesResult> allVehicles)
    {
        Success = true;
        AllVehicles = allVehicles;
    }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = String.Empty;
    public int ReturnCode;
    public List<AllVehiclesResult> AllVehicles { get; set; }    
}

public record AllVehiclesResult(int VehicleId, int Mileage, string RegistrationNumber, int CategoryId);

