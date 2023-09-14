using Bloe_web.Models.AutoMappers;
using Blog_Dal.Context;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Abstract;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web
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
            services.AddControllersWithViews();

            services.AddDbContext<ProjectContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddIdentity<AppUser, IdentityRole>
                (
                    x =>
                    {
                        x.User.RequireUniqueEmail = true;    //ki�iye ait mail adresi e�siz olcak 
                        x.Password.RequiredLength = 4;    // �ifre karakter say�s�
                        x.Password.RequireLowercase = false;   // k���k harf zorunlulu�u olsun olmas�n
                        x.Password.RequireUppercase = false;   // b�y�k harf zorlunlu�u olsun olmasn
                        x.Password.RequireNonAlphanumeric = false;   // �ifrede �rne�in !, @, #, $, vb. gibi �zel karakterler olsun olmas�n
                        x.Password.RequireDigit = false;    // en az bir say� olsun olmasn
                        x.Password.RequiredUniqueChars = 0;

                        // katmanl� mimari  migration yaparken  package manager alan�nda  default proje   project context oldu�u yer start set projede  ba�lant� bilgilerinin oldu�u bu proje olmal�


                    }

                ).AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();


            services.AddAutoMapper(typeof(Mappers));
            services.AddScoped<IAppUserRepo, AppUserRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IArticleRepo, ArticleRepo>();

            services.AddAuthentication();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {

                //localhost/----areaname----/controllername/actionname/paramtere �eklilnde arealar olmal� 

                endpoints.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
