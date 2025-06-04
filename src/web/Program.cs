using OpenGolfCoach.Application;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Gpx;
using OpenGolfCoach.Infrastructure.OpenStreetmap;
using OpenGolfCoach.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new CoordinateJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGolfCourseRetriever, GolfCourseRetriever>();
builder.Services.AddScoped<IFromGpxImplementation, FromGpxImplementation>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();

app.Run();
