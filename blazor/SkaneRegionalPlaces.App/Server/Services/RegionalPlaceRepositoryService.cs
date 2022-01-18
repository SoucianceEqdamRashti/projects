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
        public string StorageConnectionString { get; set; } = "DefaultEndpointsProtocol=https;AccountName=souciblazorappstorage;AccountKey=9iLjYeC+izhHuoZaCdvruLNztUh4hbv3tzkFY3Z3m0u3VNLWZrzt8dW12wN5q+m4IeH3ISBfF6ZfkYAa/bA/cg==;EndpointSuffix=core.windows.net";

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
