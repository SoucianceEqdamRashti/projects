using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkaneRegionalPlaces.App.Shared
{
    public class RegionalPlace : ITableEntity
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }        
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

    }
}
