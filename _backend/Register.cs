using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MemoryLeek.Requests;
using MemoryLeek.Models;
using System.Linq;

namespace MemoryLeek.Function
{
	public static class Register
	{
		[FunctionName("Register")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]RegistrationRequest req,
			HttpRequest request,
			[CosmosDB(
				databaseName: "Visitors",
				containerName: "Visitors",
				Connection = "CosmosDBConnection")]
				IAsyncCollector<Visitor> visitors)
		{
			if (string.IsNullOrEmpty(req.Nick)
				|| req.Nick.Length > 30
				|| req.Group?.Length > 30
				|| req.Email?.Length > 320
				|| req.Extra?.Length > 1024
				)
			{
				return new BadRequestResult();
			}

			await visitors.AddAsync(new Visitor
			{
				Nick = req.Nick,
				Group = req.Group,
				Email = req.Email,
				Extra = req.Extra,
				Ip = request.Headers["X-Forwarded-For"].FirstOrDefault(),
			});

			return new OkResult();
		}
	}
}
