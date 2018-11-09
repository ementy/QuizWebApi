using Data;
using Data.Repositories;
using Data.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.Data.Repositories;
using System.IO;

namespace QuizApi
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
			//Added the QuizDbContext as a service. Uses SqlServer
			services.AddDbContext<QuizDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("QuizDatabase")));

			//Transients or Singletons?
			services.AddTransient<IAuthorRepository, AuthorRepository>();
			services.AddTransient<IQuoteRepository, QuoteRepository>();

			services.AddRouting();

			//First try adding Swagger
			services.AddSwaggerGen(swagger =>
			{
				swagger.DescribeAllParametersInCamelCase();
				swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "QuizApiSwagger" });
				swagger.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "QuizApi.xml"));
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizApiSwagger");
			});

			app.UseMvc();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
