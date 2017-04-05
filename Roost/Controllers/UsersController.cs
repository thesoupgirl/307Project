using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Roost.Interfaces;
using Roost.Models;
using System.IO;
using System.Text;

namespace Roost.Controllers
{
	[Route("api/users")]
	public class UsersController : Controller
	{

		private readonly IUserRepository _userRepository;
		DBHelper db = new DBHelper();

		public UsersController(IUserRepository userRepository)

		{
			_userRepository = userRepository;
			//Congrats, I'm initialized!
			//TODO:  Initialize DBHelper here bruh
		}
		//[HttpGet]
		//public IActionResult List()
		//{
		//	return Ok(_userRepository.All);
		//}

		//route is localhost:5000/api/users
		//this route actually works...       
		[HttpGet]
		public async Task<String> Get()
		{
			try
			{
				//await db.client.GetItemAsync(
				//tableName: "User",
				//key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
				//{	
				//{"userId", new AttributeValue {S = "777"} },
				//{"displayName", new AttributeValue {S = "joe"} },
				//{"password", new AttributeValue {S = "blah"} }
				//}
				return "Soup";
				// );
				//return "User exists";
			}
			catch (Exception)
			{
				return "Error: Incorrect username or password";
			}

			return "rawr";
		}
		// GET: /api/users/login
		// Sign-in the user
		[HttpGet("login/{id}/{passHash}")]
		public async Task<HttpResponseMessage> Login(String id, String passHash)
		{
			//this takes request parameters only from the query string
			try
			{
				//Console.WriteLine("meow " + id);
				//Console.WriteLine("meow" + qsList.size());
				//System.Diagnostics.Debug.WriteLine("arf");
				//string[] array = new string[queryStrings.Count];

				//queryStrings.CopyTo(array, 0);

				GetItemResponse stuff = await db.client.GetItemAsync(
					tableName: "User",
					key: new Dictionary<string, AttributeValue>
					{
						{"userId", new AttributeValue {S = id} },
						{"displayName", new AttributeValue {S = id} },
						//{"password", new AttributeValue {S = "202cb962ac59075b964b07152b234b70"} }
					}
				);
				if (stuff.Item["password"].S == passHash)
				{
					Response.StatusCode = 200;
					HttpResponseMessage response = new HttpResponseMessage();
					response.Content = new StringContent("distance=" + stuff.Item["distance"].S);
					return response;
				}
				else
				{
					Response.StatusCode = 400;
					HttpResponseMessage response = new HttpResponseMessage();
					return response;

				}
				//return "meow";
			}
			catch (Exception)
			{
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
			}
		}

		// GET: /api/users/login
		// Creates a new user
		[HttpPost("login")]
		public async Task<HttpResponseMessage> Login()
		{
			//this takes request parameters only from the query string
			//Workaround - copy original Stream
			try
			{
				string username = Request.Form["username"];
				string password = Request.Form["password"];
				Console.WriteLine(username);
				Console.WriteLine(password);
				PutItemResponse stuff = await db.client.PutItemAsync(
					tableName: "User",
					item: new Dictionary<string, AttributeValue>
					{
						{"userId", new AttributeValue {S = username} },
						{"displayName", new AttributeValue {S = username} },
						{"password", new AttributeValue {S = password} },
						{"distance", new AttributeValue {S = "5"} }
					}
				);

				Response.StatusCode = 200;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
				//if(stuff.Item["password"].S == passHash) {
				//    Response.StatusCode = 200;
				//    HttpResponseMessage response = new HttpResponseMessage();
				//    return response;
				//}
				//else {
				//    Response.StatusCode = 400; 
				//    HttpResponseMessage response = new HttpResponseMessage();
				//    return response;

				//}       
				//return "meow";
			}
			catch (Exception)
			{
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
			}
		}

		// POST: /api/users/update/{id}
		// Update user info
		[HttpPost("update/{id}")]
		public async void UpdateUser(string id)
		{
			string username = Request.Form["username"];
			string password = Request.Form["password"];
			string pushNote = Request.Form["notifications"];
			string distance = Request.Form["distance"];
			Console.WriteLine("\nNotifications: ");
			Console.WriteLine(pushNote);
			int pushNot = Convert.ToInt32(pushNote);
			Console.WriteLine("\nrawr\n");
			Console.WriteLine(username);
			Console.WriteLine(password);
			Console.WriteLine("\nawks\n");

			try
			{
				//DynamoDBContext context = new DynamoDBContext(db.client);
				Console.WriteLine("trying...");
				var table = Table.LoadTable(db.client, "User");
				Console.WriteLine("found the table...");
				var item = await table.GetItemAsync(id, id);
				Console.WriteLine("\ngot the item");
				if (item == null)
				{
					// row not exists -> insert & return 1
					Console.WriteLine("\nCouldn't find user in Dynamo");
					return;
				}
				// row exists -> increment counter & update
				//var counter = item["Counter"].AsInt();
				item["password"] = password;
				item["notifications"] = pushNot;
				item["distance"] = distance;
				await table.UpdateItemAsync(item);
				Console.WriteLine("\nupdated it?  hopefully...");
				return;
				// await db.client.PutItemAsync(
				//    tableName: "User",
				//   item: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
				//  {
				//     {"userId", new AttributeValue {S = id} },
				//    {"username", new AttributeValue {S = username}},
				//   {"password", new AttributeValue {S = password}}
				//  });
			}
			catch (Exception)
			{
				Console.WriteLine("\nexception...");
			}
		}

	}
}
