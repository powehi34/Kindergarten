using AutoMapper;
using Contracts.Managers;
using Kindergarten.BLL.Managers;
using Kindergarten.BLL.Mapper;
using Kindergarten.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KindergartenContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
var config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>
    {
        new KindergartenTeacherProfile(),
        new ChildProfile(),
        new GroupTypeProfile(),
        new GroupProfile()
    }));
builder.Services.AddScoped<IMapper>(x => new Mapper(config));
builder.Services.AddSession();
var app = builder.Build();
app.UseSession();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
