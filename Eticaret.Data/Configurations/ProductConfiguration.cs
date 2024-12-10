using Eticaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Data.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
			builder.Property(x => x.Image).HasMaxLength(500);
			builder.Property(x => x.ProductCode).HasMaxLength(50);
		}
	}
}
