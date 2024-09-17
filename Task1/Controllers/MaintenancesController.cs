using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Backend.Interfaces;
using Task1.Backend.Repositories;
using Task1.Data.Models;
using Task1.DTOs;
using Task1.ViewModels;

namespace Task1.Controllers
{
	[Authorize]
	public class Maintenances : Controller
	{
		private readonly ICarRepository _carRepository;
		private readonly IMaintenanceRepository _repository;

		public Maintenances(ICarRepository carRepository, IMaintenanceRepository maintenanceRepository)
		{
			_carRepository = carRepository;
			_repository = maintenanceRepository;
		}

		public async Task<IActionResult> Index()
		{
			var maints = await _repository.GetAllMaintenances();

			return View(maints);
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.Action = "edit";
			var driverViewModel = new DriverViewModel
			{
				Maintenance = await _repository.GetMaintenanceById(id),
                CarIds = await _carRepository.GetAllCarIds()
            };

            return View(driverViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(DriverViewModel maint)
		{
			if (ModelState.IsValid)
			{
				await _repository.UpdateMaintenance(maint.Maintenance);
				return RedirectToAction(nameof(Index));
			}

            maint.CarIds = await _carRepository.GetAllCarIds();
            ViewBag.Action = "edit";
			return View(maint);
		}

		public async Task<IActionResult> Add(int? carId)
		{
			var driverViewModel = new DriverViewModel
			{
                CarIds = await _carRepository.GetAllCarIds()
			};
			if (carId != null)
			{
				driverViewModel.Maintenance.CId = carId??0;
			}

            ViewBag.Action = "add";
			return View(driverViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm] DriverViewModel maint)
		{
			if (ModelState.IsValid)
			{
				await _repository.AddMaintenance(maint.Maintenance);
                return RedirectToAction("CarDetails", "Car", new { id = maint.Maintenance.CId });
            }

            maint.CarIds = await _carRepository.GetAllCarIds();
            ViewBag.Action = "add";
			return View(maint);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			await _repository.Delete(id.HasValue ? id.Value : 0);
			return RedirectToAction(nameof(Index));
		}
	}
}
