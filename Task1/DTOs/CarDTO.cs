using Task1.Data.Models;

namespace Task1.DTOs
{
	public class CarDTO
	{
        public int Id { get; set; }
		public string Url { get; set; } = "~/Images/no-image.png";
		public string Brand { get; set; }
        public string Number { get; set; }
        public int Seats { get; set; }
        public string Color { get; set; }
		public DateOnly MDate { get; set; }
		public int SelectedDriverId { get; set; }
		public List<DriverDTO> Drivers { get; set; } = new List<DriverDTO>();
        public List<MaintenanceDTO> Maintenances { get; set; } = new List<MaintenanceDTO>();
    }
}
