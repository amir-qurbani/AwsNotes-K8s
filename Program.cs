using Amazon;                 // <-- lägg till för RegionEndpoint
using Amazon.S3;
using AwsNotes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MongoDbService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Starta på Notes/Create
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Create}/{id?}");

app.Run();