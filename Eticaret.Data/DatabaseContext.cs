using Eticaret.Core.Constants;
using Eticaret.Core.Entities;
using Eticaret.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//"Data Source=DESKTOP-M24TL3H\\SQLEXPRESS; initial catalog=EticaretDb;uid=sa;pwd=123;TrustServerCertificate=true;Trusted_Connection=true;",options =>
			//    options.CommandTimeout(180)

			optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); 
			// 'Microsoft.EntityFrameworkCore.Migrations.PendingModelChangesWarning hatası için yazıldı

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

            // Cascade delete yapılandırması
            modelBuilder.Entity<Address>()
                .HasOne(a => a.AppUser)      // Address -> AppUser ilişkisi
                .WithMany(u => u.Addresses)  // AppUser -> Addresses ilişkisi
                .HasForeignKey(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete özelliğini aktif et

            // Category ile Product arasında bire-çok ilişki ve Cascade Delete
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)    // Her ürün bir kategoriye bağlı
                .WithMany(c => c.Products)  // Her kategori birden fazla ürüne sahip olabilir
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete ayarı

            //çalışan dll in içinden bulup kendisi tüm claslar için oluşturuyor.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}
