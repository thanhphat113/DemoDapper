using PPDD.Helper;
using Microsoft.EntityFrameworkCore;
using PPDD.Models;
using PPDD.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<QlkhoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IHoaDonXuatService, HoaDonXuatService>();
builder.Services.AddScoped<IHoaDonNhapService, HoaDonNhapService>();
builder.Services.AddScoped<ILoHangService, LoHangService>();
builder.Services.AddScoped<IThongKeService, ThongKeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

