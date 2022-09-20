using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MemoryLeek.Models;
using System.Linq;

namespace MemoryLeek.Function
{
    public static class Visitors
	{
		[FunctionName("Visitors")]
		public static IActionResult Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
			[CosmosDB(
				databaseName: "Visitors",
				containerName: "Visitors",
				Connection = "CosmosDBConnection",
				SqlQuery = "SELECT * FROM c WHERE c.Year = '2022' ORDER BY c._ts ASC")]
				IEnumerable<Visitor> visitors)
		{
			return new OkObjectResult(visitors.Select(PrettyName));
		}

		private static string PrettyName(Visitor v)
		{
			return string.IsNullOrEmpty(v.Group)
				? v.Nick
				: $"{v.Nick}^{v.Group}";
		}
	}
}
