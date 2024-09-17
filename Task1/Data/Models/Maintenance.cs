using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Data.Models
{
	[Table("maintenance")]
	public class Maintenance
    {
        public int Id { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; } = string.Empty;
		public sbyte IsDeleted { get; set; } = 0;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CarId { get; set; }
    }
}
