using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task1.Data.Models;

namespace Task1.Data.Configuration
{
	public class DriverConfiguration : IEntityTypeConfiguration<Driver>
	{
		public void Configure(EntityTypeBuilder<Driver> builder)
		{
			builder.HasQueryFilter(c => c.IsDeleted == 0)
				.HasIndex(c => c.IsDeleted);
		}
	}
}
