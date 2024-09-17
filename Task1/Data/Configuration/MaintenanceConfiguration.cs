using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Task1.Data.Models;

namespace Task1.Data.Configuration
{
	public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
	{
		public void Configure(EntityTypeBuilder<Maintenance> builder)
		{
			builder.HasQueryFilter(c => c.IsDeleted == 0)
				.HasIndex(c => c.IsDeleted);
		}
	}
}