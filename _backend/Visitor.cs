using System;
using Newtonsoft.Json;

namespace MemoryLeek.Models
{
	public 	class Visitor
	{
		[JsonProperty("id")]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Nick { get; set; }
		public string Group { get; set; }
		public string Email { get; set; }
		public string Extra { get; set; }

		public string Ip { get; set; }
		public string Year { get; set; } = "2022";
	}
}
