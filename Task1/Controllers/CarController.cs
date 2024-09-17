using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Task1.Backend.Interfaces;
using Task1.Data.Models;
using Task1.DTOs;
using Task1.ViewModels;

namespace Task1.Controllers
{
    [Authorize]
	public class CarController : Controller
	{
		private readonly ICarRepository _carRepository;
		private readonly IDriverRepository _driverRepository;
		private readonly IMaintenanceRepository _maintenanceRepository;

		public CarController(ICarRepository repository, IDriverRepository driverRepository, IMaintenanceRepository maintenanceRepository)
		{
			_carRepository = repository;
			_driverRepository = driverRepository;
			_maintenanceRepository = maintenanceRepository;
		}

		public async Task<IActionResult> CarDetails(int id)
		{
			var carVewModel = new CarViewModel
			{
				CarDto = await _carRepository.GetCarById(id),
				Drivers = await _driverRepository.GetAllDrivers(),
                Maintenance = await _maintenanceRepository.GetAllMaintenances()
			};

			return View(carVewModel);
		}

		public async Task<IActionResult> Index()
		{
			var cars = await _carRepository.GetAllCars();

			return View(cars);
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.Action = "edit";

			var carViewModel = new CarViewModel
			{
				CarDto = await _carRepository.GetCarById(id),
				Drivers = await _driverRepository.GetAllDrivers()
			};

			return View(carViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CarViewModel car)
		{
			if (ModelState.IsValid)
			{
				await _carRepository.UpdateCar(car.CarDto);
				return RedirectToAction("CarDetails", new { id = car.CarDto.Id });
			}

			ViewBag.Action = "edit";
			return View(car);
		}

		public async Task<IActionResult> Add()
        {
			var carViewModel = new CarViewModel
			{
				Drivers = await _driverRepository.GetAllDrivers()
			};

			ViewBag.Action = "add";
			return View(carViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Add([FromForm] CarViewModel car)
		{
			if (ModelState.IsValid)
			{
				var carDto = car.CarDto;

				if (car.SelectedDriverId > 0)
				{
					var selectedDriver = await _driverRepository.GetDriverById(car.SelectedDriverId);
					if (selectedDriver != null)
					{
						if (carDto.Drivers == null)
						{
							carDto.Drivers = new List<DriverDTO>();
						}

						carDto.Drivers.Add(selectedDriver);
					}
				}

				await _carRepository.AddCar(carDto);
				return RedirectToAction("Index", "Home");
			}

			car.Drivers = await _driverRepository.GetAllDrivers();
			ViewBag.Action = "add";
			return View(car);
		}


		public async Task<IActionResult> Delete(int? id)
        {
            await _carRepository.Delete(id.HasValue ? id.Value : 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
