using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Exceptions
{
    public class UnknownVehicleCategoryException : Exception
    {
        public UnknownVehicleCategoryException(string message): base(message)
        {

        }
    }
}
