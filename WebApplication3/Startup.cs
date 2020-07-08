using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using WebApplication3.Implementation;
using WebApplication3.Interfaces;
using WebApplication3.Model;

namespace WebApplication3
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
            
            

             //services.AddSingleton<SignInManager<AppRole>>();
            //services.AddSingleton<UserManager<AppUser>>();
       

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddDbContext<ApplicationDBContext>(item =>
           item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDBContext>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<AuthenticationBusiness>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<ISaleInfoRepository, SalesInfoRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ClaimsFactory>();
            // services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddHttpContextAccessor();
            //services.AddScoped<ApplicationDBContext, ApplicationDBContext>();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            SetMigrate(connectionString);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleDocument", Version = "v1" });
            });

         
        }
            

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

            app.UseHttpsRedirection();
           
            app.UseAuthentication();



           InitializeRoles(serviceProvider);
            Initialize(serviceProvider);
        }

        private void SetMigrate(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var context = new ApplicationDBContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }

        public static async void InitializeRoles(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDBContext>();
            string[] roles = new string[] { "SuperAdmin", "Owner", "Admin", "Employee", "Delivery", "Customer", "Guest" };

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    var temprole = new AppRole()
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };
                    await context.Roles.AddAsync(temprole);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDBContext>();
            var user = new AppUser
            {
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = "Super Admin",
                LastName = "Super Admin"
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "123456");
                user.PasswordHash = hashed;
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var admin = await context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                var role = await context.Roles.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == "superadmin");
                var userRole = await context.UserRoles.Where(x => x.UserId == user.Id && x.RoleId == role.Id).FirstOrDefaultAsync();

                if (userRole == null)
                {
                    userRole = new UserRoles()
                    {
                        UserId = admin.Id,
                        RoleId = role.Id
                    };
                    await context.UserRoles.AddAsync(userRole);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
