using Partytime.Common.MassTransit;
using Partytime.Common.Settings;
using Partytime.Party.Service.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get service settings
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

// Use the common code and initialize what was here before
builder.Services.AddMassTransitWithRabbitMq();

// Makes it easy to communicate between microservices
builder.Services.AddHttpClient<JoinedClient>(client => {
    client.BaseAddress = new Uri("https://localhost:5005");
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
