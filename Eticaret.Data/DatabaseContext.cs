using Eticaret.Core.Constants;
using Eticaret.Core.Entities;
using Eticaret.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Data
{
	public class DatabaseContext :DbContext
	{
		public DbSet<AppUser> AppUsers {  get; set; }	
		public DbSet<Brand> Brands {  get; set; }
		public DbSet<Category> Categories {  get; set; }
		public DbSet<Contact> Contacts {  get; set; }
		public DbSet<News> News {  get; set; }
		public DbSet<Product> Products {  get; set; }
		public DbSet<Slider> Sliders {  get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            //"Data Source=DESKTOP-M24TL3H\\SQLEXPRESS; initial catalog=EticaretDb;uid=sa;pwd=123;TrustServerCertificate=true;Trusted_Connection=true;",options =>
            //    options.CommandTimeout(180)



            if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Parameters.ConnectionString, options => options.CommandTimeout(180)).EnableSensitiveDataLogging();
			}
			

			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new AppUserConfiguration());
			//modelBuilder.ApplyConfiguration(new ProductConfiguration());

			//çalışan dll in içinden bulup kendisi tüm claslar için oluşturuyor.
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}
