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
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<RentalModel>>
    {
        private readonly IDataAccessRepository _repo;

        public GetAllBookingsHandler(IDataAccessRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<RentalModel>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var allBookings = await _repo.GetAllBookingsAsync();
            return allBookings.ToList();
        }
    }
}
