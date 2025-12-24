using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Repositories.Interfaces;
using secondcharge.api.Repositories.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SecondChargeDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SecondChargeConnectionString")));

builder.Services.AddScoped<ICarRepository, SQLCarRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<ILocationRepository, SQLLocationRepository>();
builder.Services.AddScoped<IVehicleListingRepository, SQLVehicleListingRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS - must be placed before UseAuthorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
