using Task1.DTOs;

public class DriverViewModel
{
	public DriverDTO? DriverDto { get; set; }
    public MaintenanceDTO Maintenance { get; set; } = new MaintenanceDTO();
	public List<int>? CarIds { get; set; } = new List<int>();
	public int? SelectedCarId { get; set; }
}
