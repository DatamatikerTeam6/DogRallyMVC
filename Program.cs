using DogRallyMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IPostTrackToAPI, PostTrackToAPI>();
builder.Services.AddTransient<IGetExercisesFromAPI, GetExercisesFromAPI>();
builder.Services.AddTransient<IGetTrackFromAPI, GetTrackFromAPI>();
builder.Services.AddTransient<IGetUserTracksFromAPI, GetUserTracksFromAPI>();
builder.Services.AddTransient<IDeleteTrackFromAPI, DeleteTrackFromAPI>();


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