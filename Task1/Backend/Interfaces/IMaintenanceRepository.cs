using Task1.DTOs;

namespace Task1.Backend.Interfaces
{
    public interface IMaintenanceRepository
    {
		Task<List<MaintenanceDTO>> GetAllMaintenances();
		Task<MaintenanceDTO> GetMaintenanceById(int id);
		Task AddMaintenance(MaintenanceDTO maintDto);
		Task UpdateMaintenance(MaintenanceDTO maintenanceDto);
		Task Delete(int id);
	}
}