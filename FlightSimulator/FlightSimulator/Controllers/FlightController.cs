using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightController : Controller
	{
		private List<FlightData> flightDataList = new List<FlightData>();

		// GET: api/flight
		[HttpGet]
		public IActionResult Get()
		{
			// Implement logic to retrieve a list of flight data
			return Ok(flightDataList);
		}

		// GET: api/flight/{id}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			// Implement logic to retrieve flight data by ID
			var flight = flightDataList.FirstOrDefault(f => f.Id == id);
			if (flight == null)
			{
				return NotFound(); // Flight not found
			}
			return Ok(flight);
		}

		// POST: api/flight
		[HttpPost]
		public IActionResult Post([FromBody] FlightData flightData)
		{
			// Implement logic to create a new flight record
			flightData.Id = flightDataList.Count + 1;
			flightDataList.Add(flightData);
			return CreatedAtAction(nameof(GetById), new { id = flightData.Id }, flightData);
		}

		// PUT: api/flight/{id}
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] FlightData flightData)
		{
			// Implement logic to update flight data by ID
			var existingFlight = flightDataList.FirstOrDefault(f => f.Id == id);
			if (existingFlight == null)
			{
				return NotFound(); // Flight not found
			}

			existingFlight.Name = flightData.Name;
			existingFlight.Description = flightData.Description;

			return NoContent();
		}

		// DELETE: api/flight/{id}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			// Implement logic to delete flight data by ID
			var flightToRemove = flightDataList.FirstOrDefault(f => f.Id == id);
			if (flightToRemove == null)
			{
				return NotFound(); // Flight not found
			}

			flightDataList.Remove(flightToRemove);
			return NoContent();
		}
	}
}
