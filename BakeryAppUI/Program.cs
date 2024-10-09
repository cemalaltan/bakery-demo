using BakeryAppUI.Controllers;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSingleton<ITakenCourseService, TakenCourseManager>();

builder.Services.AddHttpClient<ApiService>();



//builder.Services.AddHttpClient<EndOfDayAccountService>();
//builder.Services.AddTransient<ApiService>();

builder.Services.AddScoped<EndOfDayAccountService>();
builder.Services.AddScoped<ProductionListDetailService>();


// Add services to the container.
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


app.UseEndpoints(endpoints =>
{
      
    endpoints.MapControllerRoute(
        name: "customRoute",
        pattern: "G�nSonuHesap", 
        defaults: new { controller = "EndOfDayAccount", action = "Index" } 
    );
       
    endpoints.MapControllerRoute(
        name: "customRoute",
        pattern: "AnaSayfa", 
        defaults: new { controller = "Home", action = "Index" } 
    );
    

    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
