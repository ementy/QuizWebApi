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
            //TODO: Export the connection string to a different file/class
            services.AddDbContext<QuizDbContext>(opt => opt.UseSqlServer("Server=.\\SQLEXPRESS;Database=QuizApi;Integrated Security=True;"));

            //Transients or Singletons?
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IQuoteRepository, QuoteRepository>();



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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
