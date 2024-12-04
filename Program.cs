using WorkoutProgramService.Data;
using WorkoutProgramService.Services; // Добавьте это пространство имен
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавление DbContext и конфигурация подключения к базе данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервиса IWorkoutProgramService
builder.Services.AddScoped<IWorkoutManagementService, WorkoutManagementService>(); // Убедитесь, что у вас есть реализация
// Добавляем CORS до конфигурации приложения
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7132") // Замените на ваш фронтенд-URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Позволяет передавать cookies или токены
    });
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




// Разрешаем CORS
app.UseCors("AllowSpecificOrigin");


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
