using DinkToPdf;
using DinkToPdf.Contracts;
using DottoressaIorio.App.Data;
using DottoressaIorio.App.Services;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging to a file
builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/DottoressaIorio.App.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Use Serilog for logging
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Register DinkToPdf converter
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Register PdfService
builder.Services.AddScoped<TherapyTemplateRepository>();
builder.Services.AddScoped<PatientTherapyRepository>();
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<PdfService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if (!dbContext.Database.IsRelational() || !dbContext.Database.CanConnect())
        // SQLite file doesn't exist or cannot connect, run migration
        dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // If not in development, listen on all interfaces
    //app.Urls.Add("http://0.0.0.0:5000");  // For IPv4
    //app.Urls.Add("http://[::]:5000");  // For IPv6
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();