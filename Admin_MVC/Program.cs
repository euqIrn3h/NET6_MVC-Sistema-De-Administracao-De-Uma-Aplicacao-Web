using Admin_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Profiles;
using Admin_MVC.Env;

var builder = WebApplication.CreateBuilder(args);

Env.Variaveis();
var connect = Environment.GetEnvironmentVariable("CONNECTION");
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

// Verifica Permiss�es
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acesso}/{action=Index}/{id?}");

app.Run();
