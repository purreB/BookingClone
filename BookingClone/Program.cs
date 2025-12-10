
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();


// Dependency Injection for Onion Architecture
builder.Services.AddSingleton<BookingClone.Domain.IHotelRepository, BookingClone.Infrastructure.Repositories.HotelRepository>();
builder.Services.AddSingleton<BookingClone.Domain.IHotelRoomRepository, BookingClone.Infrastructure.Repositories.HotelRoomRepository>();
builder.Services.AddSingleton<BookingClone.Domain.IUserRepository, BookingClone.Infrastructure.Repositories.UserRepository>();

builder.Services.AddSingleton<BookingClone.Application.Services.IHotelService, BookingClone.Application.Services.HotelService>();
builder.Services.AddSingleton<BookingClone.Application.Services.IHotelRoomService, BookingClone.Application.Services.HotelRoomService>();
builder.Services.AddSingleton<BookingClone.Application.Services.IUserService, BookingClone.Application.Services.UserService>();

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
