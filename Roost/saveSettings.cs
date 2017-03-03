using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using RoostApp.Controllers;
using RoostApp;

namespace Roost
{

    public class SettingsSaver
    {
        DBHelper db = new DBHelper();

        
        public void saveSettings(string id, string settingsStr)
        {
            //save settingStr
            //find user by id
            DynamoDBContext context = new DynamoDBContext(db.client);
            context.LoadAsync(new Dictionary<string, AttributeValue> { { "userID", new AttributeValue { S = id } } });
            
            context.SaveAsync(settingsStr);
        }
        

        
    }

}