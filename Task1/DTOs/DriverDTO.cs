using Task1.Data.Models;

namespace Task1.DTOs
{
	public class DriverDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateOnly Start { get; set; }
		public DateOnly End { get; set; }
		public string CurOrPre { get; set; } = string.Empty;
		public List<CarDTO> CarsDto { get; set; } = new List<CarDTO>();
	}
}
