using DAL.Helper.Interfaces;
using DAL.Helper;
using BLL.Interfaces;
using BLL;
using DAL.Interfaces;
using DAL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // Cho phép mọi origin
               .AllowAnyMethod()  // Cho phép mọi phương thức HTTP (GET, POST, PUT, DELETE, ...)
               .AllowAnyHeader(); // Cho phép mọi header
    });
});

// Thêm các service cho ứng dụng
builder.Services.AddTransient<IDatabaseHelper, DatabaseHelper>();

builder.Services.AddTransient<DAL_IKhachHang, DAL_KhachHang>();
builder.Services.AddTransient<BLL_IKhachHang, BLL_KhachHang>();

builder.Services.AddTransient<DAL_ITaiKhoan, DAL_TaiKhoan>();
builder.Services.AddTransient<BLL_ITaiKhoan, BLL_TaiKhoan>();

builder.Services.AddTransient<DAL_ITheLoai, DAL_TheLoai>();
builder.Services.AddTransient<BLL_ITheLoai, BLL_TheLoai>();

builder.Services.AddTransient<DAL_ITruyen, DAL_Truyen>();
builder.Services.AddTransient<BLL_ITruyen, BLL_Truyen>();

builder.Services.AddTransient<DAL_IGiamGia, DAL_GiamGia>();
builder.Services.AddTransient<BLL_IGiamGia, BLL_GiamGia>();

builder.Services.AddTransient<DAL_IDonHang, DAL_DonHang>();
builder.Services.AddTransient<BLL_IDonHang, BLL_DonHang>();

builder.Services.AddTransient<DAL_IGioHang, DAL_GioHang>();
builder.Services.AddTransient<BLL_IGioHang, BLL_GioHang>();


// Cấu hình các đối tượng cấu hình kiểu mạnh
IConfiguration configuration = builder.Configuration;
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// Thêm các dịch vụ cho controller
builder.Services.AddControllers();

// Thêm Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Áp dụng CORS chính sách
app.UseCors("AllowAll");  // Áp dụng chính sách CORS trước các middleware khác

app.UseAuthorization();

app.MapControllers();

app.Run();
