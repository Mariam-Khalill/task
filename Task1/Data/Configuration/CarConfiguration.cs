using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task1.Data.Models;

namespace Task1.Data.Configuration
{
	public class CarConfiguration : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.HasQueryFilter(c => c.IsDeleted == 0)
				.HasIndex(c => c.IsDeleted);
		}
	}
}