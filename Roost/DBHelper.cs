using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;

namespace Roost
{
    /// <summary>
    /// A class with methods to setup and access our database.
    /// </summary>
    public class DBHelper
    {
        public readonly AmazonDynamoDBClient client;

        /// <summary>
        /// The constructor sets the credentials and configures settings such as the region endpoint.
        /// </summary>
        public DBHelper()
        {
            var config = new ConfigurationBuilder()
            .Build();

            var dbConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast2
            };

            client = new AmazonDynamoDBClient(dbConfig);
        }
    }
}
