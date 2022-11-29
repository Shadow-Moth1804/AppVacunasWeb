using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ConfiguracionConexion>(builder.Configuration.GetSection("ConfiguracionConexion"));
builder.Services.AddScoped<IMascotaRepositorio, MascotaRepositorio>();
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
builder.Services.AddScoped<IVacunaRepositorio, VacunaRepositorio>();
builder.Services.AddScoped<IDueñoRepositorio, DueñoRepositorio>();
builder.Services.AddScoped<IHistorialRepositorio,HistorialRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
