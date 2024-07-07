using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// we give to authentication the name of scheme incase if we have multiple seheme or multiple aunthentication-
// handler to know(authentication middelware) which handler should be use to encrpte and desrilized
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDeined";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(200);

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HumanRessorcesOnly", policy => policy.RequireClaim("Departmant", "HR")
    .Requirements.Add(new CheckAge(18)));

});

builder.Services.AddSingleton<IAuthorizationHandler,CheckAgeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
