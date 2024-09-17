using Task1.Data.Models;
using Task1.DTOs;

namespace Task1.ViewModels
{
    public class CarViewModel
    {
        public List<DriverDTO>? Drivers { get; set; }
        public List<MaintenanceDTO> Maintenance { get; set; } = new List<MaintenanceDTO>();
		public CarDTO? CarDto { get; set; }
		public int SelectedDriverId { get; set; }
	}
}