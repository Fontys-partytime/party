using Microsoft.AspNetCore.Hosting;
using Partytime.Common.MassTransit;
using Partytime.Common.Settings;
using Partytime.Party.Service.Clients;
using Partytime.Party.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get service settings
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

// Use the common code and initialize what was here before
builder.Services.AddMassTransitWithRabbitMq();

// Makes it easy to communicate between microservices
// builder.Services.AddHttpClient<JoinedClient>(client => {
//     client.BaseAddress = new Uri("https://localhost:5004");
// });


builder.Services.AddControllers();
builder.Services.AddScoped<IPartyRepository, PartyRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ THIS IS COMMMENTED UNTILL THE END OF THE DEVELOPMENT PHASE SO YOU CAN TEST SWAGGER ON DOCKER
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
