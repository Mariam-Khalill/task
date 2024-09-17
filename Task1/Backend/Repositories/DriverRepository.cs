using Task1.Backend.Interfaces;
using Task1.DTOs;
using Task1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Task1.Backend.Repositories
{
    public class DriverRepository : IDriverRepository
	{
		private readonly ApplicationDbContext _context;

		public DriverRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<List<DriverDTO>> GetAllDrivers()
        {
            var drivers = await _context.Drivers
                .Select(e => new DriverDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.StartDate,
                    End = e.EndDate,
                    CurOrPre = e.Status,
                    CarsDto = e.Car.Select(c => new CarDTO
					{
						Id = c.Id,
						Brand = c.Type,
						Url = c.ImageUrl,
						Number = c.PlateNumber,
						Seats = c.NumberOfSeats,
						Color = c.Color,
						MDate = c.ManufactureDate
					}).ToList()
				}).ToListAsync();

            return drivers;
        }
        
        public async Task<DriverDTO> GetDriverById(int id)
		{
			var driver = await _context.Drivers
				.Where(d => d.Id == id)
				.Select(d => new DriverDTO
				{
					Id = d.Id,
					Name = d.Name,
					Start = d.StartDate,
					End = d.EndDate,
					CurOrPre = d.Status
				}).FirstOrDefaultAsync();

			return driver;
		}

        public async Task AddDriver(DriverDTO driverDto)
        {
            var driver = new Driver
            {
                Id = driverDto.Id,
                Name = driverDto.Name,
                StartDate = driverDto.Start,
                EndDate = driverDto.End,
                Status = driverDto.CurOrPre
            };


            _context.Add(driver);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDriver(DriverDTO driverDto)
        {
            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.Id == driverDto.Id);

            if (driver != null)
            {
                driver.Name = driverDto.Name;
                driver.StartDate = driverDto.Start;
                driver.EndDate = driverDto.End;
                driver.Status = driverDto.CurOrPre;

                _context.Drivers.Update(driver);
                await _context.SaveChangesAsync();
            }
            
        }
        
        public async Task Delete(int id)
		{
			var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);

			if (driver != null)
			{
				driver.IsDeleted = 1;

				await _context.SaveChangesAsync();
			}


		}
	}
}
