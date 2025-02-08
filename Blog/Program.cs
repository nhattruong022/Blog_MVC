using Blog.Repository;
using Microsoft.EntityFrameworkCore;    

var builder = WebApplication.CreateBuilder(args);

//cấu hình sql server
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectDb"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();


//cấu hình xác thực với cookie authentication
builder.Services.AddAuthentication(options =>
{
    //mặc định là usercookie
    options.DefaultScheme = "UserCookie";
    options.DefaultChallengeScheme = "UserCookie";
})//thêm cookie User
.AddCookie("UserCookie", options =>
{
    options.Cookie.Name = "UserCookie";
    options.LoginPath = "/Customer/Login";
    options.AccessDeniedPath = "/AccessDenied";
})
.AddCookie("AdminCookie", options =>
{
    options.Cookie.Name = "AdminCookie";
    options.LoginPath = "/Customer/Login";
    options.AccessDeniedPath = "/AcessDenied";
});


//cấu hình phân quyền user và admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});







var app = builder.Build();

app.UseAuthentication(); //cấu hình xác thực
app.UseAuthorization(); //cấu hình phân quyền


//cấu hình route Admin
app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

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

app.MapControllerRoute(
    name: "Customer",
    pattern: "{controller=Customer}/{action=SignUp}/{id?}");
 



app.Run();
