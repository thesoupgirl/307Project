using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Amazon.DynamoDBv2.Model;
using System.Net.Http;
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

			// return "rawr";
		}

		// GET: /api/users/login/{id}/{passHash}
		// Sign-in the user
		[HttpGet("login/{id}/{passHash}")]
		public async Task<ContentResult> Login(String id, String passHash)
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
						{"displayName", new AttributeValue {S = id} }
					}
				);
				if (stuff.Item["password"].S == passHash)
				{
					Response.StatusCode = 200;
					HttpResponseMessage response = new HttpResponseMessage();
					//HttpResponseMessage responset = Request.CreateResponse<string>(HttpStatusCode.OK, "meow");
					//return Request.CreateResponse<HttpResponseMessage>(HttpStatusCode.OK, (HttpResponseMessage)Convert.ChangeType("meow", typeof(HttpResponseMessage)));
					//HttpResponseMessage responset = new HttpResponseMessage( HttpStatusCode.OK, new StringContent( "Your message here" ) );
					//response.Content = new StringContent("distance: ");
						//+ stuff.Item["distance"].S);
					Console.WriteLine(stuff.Item["distance"].S);
					Console.WriteLine(stuff.Item["notificatons"].N);
					//return Content("meowo");
					return Content("{ \"data\" : [ { \"distance\" : " + stuff.Item["distance"].S + ", \"notificatons\" : " + stuff.Item["notificatons"].N + " } ] }");
					//eturn Content("meow");
					//response.RequestMessage.set("distance");
					//return Request.CreateResponse(HttpStatusCode.OK,"File was processed.");
					//return response;
				}
				else
				{
					Response.StatusCode = 400;
					Console.WriteLine("in else");
					HttpResponseMessage response = new HttpResponseMessage();
					//return response;
					return null;
				}
				//return "meow";
			}
			catch (Exception)
			{
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				//return response;
				Console.WriteLine("caught booty");
				return null;
			}
		}

		// POST: /api/users/login
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
						{"distance", new AttributeValue {S = "5"} },
						{"notificatons", new AttributeValue {N = "0"} }
					}
				);

				Response.StatusCode = 200;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
			}
			catch (Exception)
			{
				Console.WriteLine("exception caught");
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
			}
		}

		// POST api/users/{userId}/{favorite}
		// add favorite to user favorites
		[HttpPost("{userId}/{favorite}")]
		public async Task<HttpResponseMessage> Favorite(string userId, string favorite)
		{
			try
			{
				var table = Table.LoadTable(db.client, "User");
				var item = await table.GetItemAsync(userId, userId);

				List<string> favorites = item["favorite"].AsListOfString();

				if (!favorites.Contains(favorite))
				{
					item["favorite"] = favorite;
					await table.UpdateItemAsync(item);
				}

				Response.StatusCode = 200;
				HttpResponseMessage response = new HttpResponseMessage();
				return response;
			}
			catch(Exception)
			{
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				Console.WriteLine("\nexception\n");
				return response;
			}
		}
	
		// POST: /api/users/update/{id}
		// Update user info
		[HttpPost("update/{id}")]
		public async Task<HttpResponseMessage> UpdateUser(string userId)
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
			Console.WriteLine("fuck"+ distance);
			Console.WriteLine("\nawks\n");

			try
			{
				Console.WriteLine("trying...");
				var table = Table.LoadTable(db.client, "User");
				Console.WriteLine("found the table...");
				var item = await table.GetItemAsync(userId, userId);
				Console.WriteLine("\ngot the item");
				if (item == null)
				{
					// row not exists -> insert & return 1
					Console.WriteLine("\nCouldn't find user in Dynamo");

					Response.StatusCode = 400;
					HttpResponseMessage response = new HttpResponseMessage();
					return response;
				}
				// row exists -> increment counter & update
				//var counter = item["Counter"].AsInt();
				item["password"] = password;
				item["notificatons"] = pushNot;
				item["distance"] = distance;
				await table.UpdateItemAsync(item);
				Console.WriteLine("\nupdated it?  hopefully...");

				Response.StatusCode = 200;
				HttpResponseMessage responsey = new HttpResponseMessage();
				return responsey;

			}
			catch (Exception)
			{
				Response.StatusCode = 400;
				HttpResponseMessage response = new HttpResponseMessage();
				Console.WriteLine("\nexception...");
				return response;
			}
		}
	}
}
