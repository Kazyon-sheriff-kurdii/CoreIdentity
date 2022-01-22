using CoreIdentity.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth",
    opt => {
        opt.Cookie.Name = "MyCookieAuth";
        opt.LoginPath = "/Account/Login";
        });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminFullAccess", policy =>
        policy.RequireClaim("Engineer")
              .Requirements.Add(new AdminFullAccessRequirment(3))
        );
});

builder.Services.AddSingleton<IAuthorizationHandler, AdminFullAccessRequirmentResultHandeler>();

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

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//TODO: authentication custome challenge , forbdin and success