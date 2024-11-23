using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MyNhaTro.Data;
using MyNhaTro.Repositories;
using System.Text;
using System.IO;
using MyNhaTro.Contracts;
using Dapper;
using MyNhaTro.Helper;
using MyNhaTro.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Cấu hình cho phép truy cập API
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//Đăng ký Identity ApplicationUser
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<QuanLyPhongTroContext>().AddDefaultTokenProviders();

//Đọc chuỗi kết nối
builder.Services.AddDbContext<QuanLyPhongTroContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KetNoiCSDL"));
});


//Đăng ký Mapper
builder.Services.AddAutoMapper(typeof(Program));


//Đăng ký repository
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Đăng ký TypeHandler
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
SqlMapper.AddTypeHandler(new NullableDateOnlyTypeHandler());
// Thiết lập ánh xạ tùy chỉnh trước khi thực hiện truy vấn
SqlMapper.SetTypeMap(typeof(CustomerModel), new CustomPropertyTypeMap(typeof(CustomerModel),
            (type, columnName) => type.GetProperty(columnName.ToPascalCase(), BindingFlags.Public | BindingFlags.Instance))
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
