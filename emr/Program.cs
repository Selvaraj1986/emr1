using emr.Models;
using emr.Models.Access_Logs;
using emr.Models.DBContext;
using emr.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var ConnectionStringsTracker = builder.Configuration.
                       GetConnectionString("EmrDb");
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                 .AllowAnyMethod()
                                                                  .AllowAnyHeader()));
// Add services to the container.
builder.Services.AddScoped<IMedications, Medications>();
builder.Services.AddScoped<IAccounts, Accounts>();
builder.Services.AddScoped<IDosages, Dosages>();
builder.Services.AddScoped<IProviders, Providers>();
builder.Services.AddScoped<IPatients, Patients>();
builder.Services.AddScoped<IAdmittingDiagnoses, AdmittingDiagnoses>();
builder.Services.AddScoped<ICodeStatuses, CodeStatuses>();
builder.Services.AddScoped<IPrecautions, Precautions>();
builder.Services.AddScoped<IAllergy, Allergy>();
builder.Services.AddScoped<IPatientMedications, PatientMedications>();
builder.Services.AddScoped<IHeight, Height>();
builder.Services.AddScoped<IRooms, Rooms>();
builder.Services.AddScoped<IWeights, Weights>();
builder.Services.AddScoped<IDailyActivities, DailyActivities>();
builder.Services.AddScoped<ITreatments, Treatments>();
builder.Services.AddScoped<IConsults, Consults>();
builder.Services.AddScoped<IDietaries, Dietaries>();
builder.Services.AddScoped<IProviderOrders, ProviderOrders>();
builder.Services.AddScoped<INotes, Notes>();
builder.Services.AddScoped<IAdmission, Admission>();
builder.Services.AddScoped<IObAdmission, ObAdmission>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<EmrDbContext>
(options => options.UseSqlServer(ConnectionStringsTracker));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmrDbContext>(opt =>
{
    opt.EnableSensitiveDataLogging();
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LoggingFilterAttribute>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default",
    pattern: "{controller=Account}/{action=index}");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");
app.Run();
