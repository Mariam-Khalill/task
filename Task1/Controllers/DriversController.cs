using System.Drawing;
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
	public class DriversController : Controller
	{
		private readonly ICarRepository _carRepository;
		private readonly IDriverRepository _repository;

		public DriversController(ICarRepository carRepository, IDriverRepository driverRepository)
		{
			_carRepository = carRepository;
			_repository = driverRepository;
		}

		public async Task<IActionResult> Index()
		{
			var drivers = await _repository.GetAllDrivers();

			return View(drivers);
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.Action = "edit";
			var driver = await _repository.GetDriverById(id);

			return View(driver);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(DriverDTO driver)
		{
			if (ModelState.IsValid)
			{
				await _repository.UpdateDriver(driver);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Action = "edit";
			return View(driver);
		}

		public IActionResult Add()
		{
			ViewBag.Action = "add";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm] DriverDTO driver)
		{
			if (ModelState.IsValid)
			{
				await _repository.AddDriver(driver);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Action = "add";
			return View(driver);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			await _repository.Delete(id.HasValue ? id.Value : 0);
			return RedirectToAction(nameof(Index));
		}
	}
}
