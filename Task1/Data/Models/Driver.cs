using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Data.Models
{
	[Table("driver")]
	public class Driver
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
		public sbyte IsDeleted { get; set; } = 0;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string Status { get; set; } = string.Empty;
        public List<Car>? Car { get; set; } = new List<Car>();
    }
}
