using Amazon;                 // <-- lägg till för RegionEndpoint
using Amazon.S3;
using Amazon.DynamoDBv2;
using AwsNotes.Services;
using AwsNotes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- AWS-klienter (MANUELL DI, inget extra paket behövs) ---
builder.Services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(RegionEndpoint.EUWest1));
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.EUWest1));

// Våra tjänster
builder.Services.AddScoped<S3Service>();
builder.Services.AddScoped<NotesRepository>();
builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseInMemoryDatabase("NotesDb"));

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
