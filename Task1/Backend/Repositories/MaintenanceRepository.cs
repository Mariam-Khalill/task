using Task1.Backend.Interfaces;
using Task1.DTOs;
using Task1.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Task1.Backend.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
	{
		private readonly ApplicationDbContext _context;

		public MaintenanceRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<MaintenanceDTO>> GetAllMaintenances()
		{
			var maints = await _context.Maintenances
				.Select(e => new MaintenanceDTO
				{
					Id = e.Id,
					MDate = e.MaintenanceDate,
					Desc = e.Description,
					CId = e.CarId
				}).ToListAsync();

			return maints;
		}

		public async Task<MaintenanceDTO> GetMaintenanceById(int id)
		{
			var maint = await _context.Maintenances
				.Where(m => m.Id == id)
				.Select(m => new MaintenanceDTO
				{
					Id = m.Id,
					MDate = m.MaintenanceDate,
					Desc = m.Description,
					CId = m.CarId
				}).FirstOrDefaultAsync();

			return maint;
		}

		public async Task AddMaintenance(MaintenanceDTO maintDto)
		{
			var maint = new Maintenance
			{
				MaintenanceDate = maintDto.MDate,
				Description = maintDto.Desc,
				CarId = maintDto.CId
			};

			var car = await _context.Cars.Include(c => c.Maintenances).FirstOrDefaultAsync(c => c.Id == maint.CarId);

			if (car != null)
			{
				car.Maintenances.Add(maint);
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Car not found");
			}
		}


		public async Task UpdateMaintenance(MaintenanceDTO maintenanceDto)
		{
			var maint = await _context.Maintenances
				.FirstOrDefaultAsync(c => c.Id == maintenanceDto.Id);

			if (maint != null)
			{
				maint.Id = maintenanceDto.Id;
				maint.MaintenanceDate = maintenanceDto.MDate;
				maint.Description = maintenanceDto.Desc;
				maint.CarId = maintenanceDto.CId;

				await _context.SaveChangesAsync();
			}
		}

		public async Task Delete(int id)
		{
			var maint = await _context.Maintenances.FirstOrDefaultAsync(d => d.Id == id);

			if (maint != null)
			{
				maint.IsDeleted = 1;

				await _context.SaveChangesAsync();
			}


		}
	}
}
