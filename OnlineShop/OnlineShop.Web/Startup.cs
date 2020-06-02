using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Data.Repositories;
using OnlineShop.Data.Models;
using OnlineShop.Services.AdminServices;
using AutoMapper;
using OnlineShop.Services.Mapping;

namespace OnlineShop.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			AssignDbContexts(services);
			AssignAutoMapper(services);
			AssignRepositories(services);
			AssignServices(services);

			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddRouting(options => options.LowercaseUrls = true);
		}

		private void AssignDbContexts(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
							options.UseSqlServer(
								Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<OnlineShop.Data.OnlineShopDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>
					(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();
		}

		private static void AssignAutoMapper(IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(mc => {
				mc.AddProfile(new MappingDtoProfile());
			});

			services.AddSingleton(mappingConfig.CreateMapper());
		}

		private static void AssignRepositories(IServiceCollection services)
		{
			services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<ISubcategoryRepository, SubcategoryRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();
		}

		private static void AssignServices(IServiceCollection services)
		{
			services.AddTransient<ICategoriesService, CategoriesService>();
			services.AddTransient<ISubcategoriesService, SubcategoriesService>();
			services.AddTransient<IProductsService, ProductsService>();
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
