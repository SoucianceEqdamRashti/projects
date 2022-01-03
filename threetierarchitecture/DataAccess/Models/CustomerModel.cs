using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Models
{
    public class CustomerModel
    {
       public int CustomerId { get; set; }
       public string PersonNumber { get; set; }
       public string CustomerCreationDate { get; set; } = DateTime.UtcNow.ToString();
    }
}
