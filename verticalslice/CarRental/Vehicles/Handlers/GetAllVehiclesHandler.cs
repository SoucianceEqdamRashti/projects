using BookingApi.Bookings.Models;
using BookingApi.Bookings.Queries;
using BookingApi.Bookings.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Handlers
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, List<GetAllVehiclesCommandResult>>
    {
        readonly IDataAccessRepository _repo;
        public GetAllVehiclesHandler(IDataAccessRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<GetAllVehiclesCommandResult>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var allVehicles = await _repo.GetAllVehiclesAsync();
            return allVehicles.ToList();
        }
    }
}
