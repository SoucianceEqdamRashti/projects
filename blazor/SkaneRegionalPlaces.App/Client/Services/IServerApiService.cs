using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Services
{
    public interface IServerApiService
    {
        Task<IEnumerable<RegionalPlace>> GetAllRegionalPlaces();
        Task<bool> AddRegionalPlace(RegionalPlace place);
    }
}
