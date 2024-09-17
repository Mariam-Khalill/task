using Task1.Data.Models;

namespace Task1.DTOs
{
	public class MaintenanceDTO
	{
		public int Id { get; set; }
		public DateTime MDate { get; set; }
		public string Desc { get; set; } = string.Empty;
		public int CId { get; set; }
	}
}
