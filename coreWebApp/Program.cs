using coreWebApp.Controllers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddHttpClient<BookController>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:44317/api");
//});

builder.Services.AddControllersWithViews();

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

//CORS ayarlar�
app.UseCors(builder =>
{
builder.AllowAnyOrigin(); // Herhangi bir origin'e izin verir. G�venlik gereksinimlerinize g�re �zelle�tirebilirsiniz.
builder.AllowAnyMethod(); // Herhangi bir HTTP methoduna izin verir. �htiyaca g�re d�zenleyebilirsiniz.
builder.AllowAnyHeader(); // Herhangi bir HTTP ba�l���na izin verir. �htiyaca g�re d�zenleyebilirsiniz.
});

app.Run();