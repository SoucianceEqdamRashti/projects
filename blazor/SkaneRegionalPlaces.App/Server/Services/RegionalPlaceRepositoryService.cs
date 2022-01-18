using Azure;
using Azure.Data.Tables;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Server.Services
{
    public class RegionalPlaceRepositoryService
    {
        public string StorageConnectionString { get; set; } = "storage connection string";

        public IEnumerable<RegionalPlace> GetRegionalPlaces()
        {
            return GetTableClient();
        }
        private IEnumerable<RegionalPlace> GetTableClient()
        {
            var tableClient = new TableClient(StorageConnectionString,"regionalplaces");
            // Create the table in the service.
            return GetEntities(tableClient);
        }

        private IEnumerable<RegionalPlace> GetEntities(TableClient tableClient)
        {
            Pageable<RegionalPlace> queryResultsSelect = tableClient.Query<RegionalPlace>(select: new List<string>() 
            {   "Location",
                "Description",
                "Address",
                "Name",
                "Url",
                "Tags",
                "Longitude",
                "Latitude",
                });         
            return queryResultsSelect.AsEnumerable<RegionalPlace>();
        }
    }
}
