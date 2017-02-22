using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;

namespace RoostApp
{
    /// <summary>
    /// A class with methods to setup and access our database.
    /// </summary>
    public class DBHelper
    {
        private readonly AmazonDynamoDBClient client;
        public DBHelper()
        {
            var config = new ConfigurationBuilder()
            .AddUserSecrets()
            .Build();

            // You may have to add these values to your secrets.json file first.
            var accessKey = config["AWSAccessKey"];
            var secretKey = config["AWSSecretKey"];

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            var dbConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast2
            };

            client = new AmazonDynamoDBClient(credentials, dbConfig);
        }
    }
}
