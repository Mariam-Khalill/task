using Task1.DTOs;

namespace Task1.Backend.Interfaces
{
	public interface ICarRepository
	{
		Task<List<CarDTO>> GetAllCars();
		Task<List<int>> GetAllCarIds();
		Task<CarDTO> GetCarById(int id);
        Task AddCar(CarDTO car);
        Task UpdateCar(CarDTO car);
        Task Delete(int id);
    }
}
