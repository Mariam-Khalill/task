using Task1.Backend.Interfaces;
using Task1.DTOs;
using Task1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Task1.Backend.Repositories
{
	public class CarRepository : ICarRepository
	{
		private readonly ApplicationDbContext _context;

		public CarRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<int>> GetAllCarIds()
		{
            var carIds = await _context.Cars.Select(c => c.Id).ToListAsync();
            return carIds;
        }


		public async Task<List<CarDTO>> GetAllCars()
        {
            var cars = await _context.Cars
                .Select(c => new CarDTO
                {
                    Id = c.Id,
                    Brand = c.Type,
                    Url = c.ImageUrl,
                    Number = c.PlateNumber,
                    Seats = c.NumberOfSeats,
                    Color = c.Color,
                    MDate = c.ManufactureDate,
                    Drivers = c.Drivers.Select(d => new DriverDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Start = d.StartDate,
                        End = d.EndDate,
                        CurOrPre = d.Status
                    }).ToList(),
					Maintenances = c.Maintenances.Select(d => new MaintenanceDTO
					{
						Id = d.Id,
						MDate = d.MaintenanceDate,
						Desc = d.Description,
						CId = d.CarId
					}).ToList()
				}).ToListAsync();

            return cars;
        }


        public async Task<CarDTO> GetCarById(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Drivers) 
                .Where(c => c.Id == id)
                .Select(c => new CarDTO
                {
                    Id = c.Id,
                    Brand = c.Type,
                    Url = c.ImageUrl,
                    Number = c.PlateNumber,
                    Seats = c.NumberOfSeats,
                    Color = c.Color,
                    MDate = c.ManufactureDate,
                    Drivers = c.Drivers.Select(d => new DriverDTO 
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Start = d.StartDate,
                        End = d.EndDate,
                        CurOrPre = d.Status
                    }).ToList()
                }).FirstOrDefaultAsync();

            return car;
        }

        public async Task AddCar(CarDTO carDto)
        {
            var car = new Car
            {
                Type = carDto.Brand,
                ImageUrl = carDto.Url,
                PlateNumber = carDto.Number,
                NumberOfSeats = carDto.Seats,
                Color = carDto.Color,
                ManufactureDate = carDto.MDate
            };

            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            if (carDto.Drivers != null && carDto.Drivers.Any())
            {
                foreach (var driverDto in carDto.Drivers)
                {
                    var driver = new Driver
                    {
                        Id = driverDto.Id, 
                        Name = driverDto.Name,
                        StartDate = driverDto.Start,
                        EndDate = driverDto.End,
                        Status = driverDto.CurOrPre
                    };
                    car.Drivers.Add(driver);
				}
				
				_context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
        }



        public async Task UpdateCar(CarDTO carDto)
        {
            var car = await _context.Cars
                .Include(c => c.Drivers) 
                .FirstOrDefaultAsync(c => c.Id == carDto.Id);

            if (car != null)
            {
                car.PlateNumber = carDto.Number;
                car.NumberOfSeats = carDto.Seats;
                car.Color = carDto.Color;
                car.Type = carDto.Brand;
                car.ImageUrl = carDto.Url;
                car.ManufactureDate = carDto.MDate;

                if (carDto.Drivers != null)
                {
                    car.Drivers.Clear();

                    foreach (var driverDto in carDto.Drivers)
                    {
                        var driver = new Driver
                        {
                            Id = driverDto.Id,
                            Name = driverDto.Name,
                            StartDate = driverDto.Start,
                            EndDate = driverDto.End,
                            Status = driverDto.CurOrPre
                        };
                        car.Drivers.Add(driver);
                    }
                }

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
        }


        public async Task Delete(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car != null)
            {
                car.IsDeleted = 1;

                await _context.SaveChangesAsync();
            }


        }
	}
}
