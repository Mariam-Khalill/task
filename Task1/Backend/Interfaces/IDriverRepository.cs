using Task1.DTOs;

namespace Task1.Backend.Interfaces
{
    public interface IDriverRepository
    {
		Task<List<DriverDTO>> GetAllDrivers();
		Task<DriverDTO> GetDriverById(int id);
		Task AddDriver(DriverDTO driver);
		Task UpdateDriver(DriverDTO driver);
		Task Delete(int id);
	}
}