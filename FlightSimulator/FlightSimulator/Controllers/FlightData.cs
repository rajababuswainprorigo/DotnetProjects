using System;
namespace FlightSimulator.Controllers
{
	public class FlightData
	{
		public int Id { get; set; }
		public string FlightNumber { get; set; }
		public string DepartureCity { get; set; }
		public string ArrivalCity { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public object Name { get; internal set; }
		public object Description { get; internal set; }
	}
}


