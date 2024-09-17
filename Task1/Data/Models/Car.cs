using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task1.DTOs;

namespace Task1.Data.Models
{
	[Table("car")]
	public class Car
    {
        public int Id { get; set; }
		public string PlateNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "~/Images/no-image.png";
		public bool Available { get; set; } = true;
        public DateOnly ManufactureDate { get; set; }
        public sbyte IsDeleted { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public List<Maintenance>? Maintenances { get; set; } = new List<Maintenance>();
		public List<Driver>? Drivers { get; set; } = new List<Driver>();

	}
}
