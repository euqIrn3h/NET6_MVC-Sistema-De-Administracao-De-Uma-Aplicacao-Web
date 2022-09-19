using Admin_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Profiles;

var builder = WebApplication.CreateBuilder(args);
var connect = builder.Configuration.GetConnectionString("ProductionDbString");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connect, ServerVersion.AutoDetect(connect)));
builder.Services.AddAutoMapper(typeof(UsuarioProfile));

//addcors
builder.Services.AddCors();

var app = builder.Build();


app.UseStaticFiles();


app.UseRouting();

app.UseCors(policy =>
            policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

// Verifica Permissões
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acesso}/{action=Index}/{id?}");

app.Run();
