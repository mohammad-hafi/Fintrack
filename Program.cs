using fintrack.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var envFilePath = Path.Combine(builder.Environment.ContentRootPath, ".env");
if (!File.Exists(envFilePath))
{
    envFilePath = Path.Combine(builder.Environment.ContentRootPath, ".Env");
}
if (File.Exists(envFilePath))
{
    var envValues = File.ReadAllLines(envFilePath)
        .Where(line => !string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("#"))
        .Select(line => line.Split('=', 2))
        .Where(parts => parts.Length == 2)
        .Select(parts => new KeyValuePair<string, string?>(parts[0].Trim(), parts[1].Trim()));

    builder.Configuration.AddInMemoryCollection(envValues);
}

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(defaultConnection))
{
    var host = builder.Configuration["DB_HOST"];
    var port = builder.Configuration["DB_PORT"];
    var database = builder.Configuration["DB_DATABASE"];
    var username = builder.Configuration["DB_USERNAME"];
    var password = builder.Configuration["DB_PASSWORD"];

    defaultConnection = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        defaultConnection,
        npgsql => npgsql.EnableRetryOnFailure()
    )
);
var app = builder.Build();

app.UseSession();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
// app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
