using Goognet.Api.Configurations;
using Goognet.Application.Interfaces;
using Goognet.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ISiteService,SiteService>();

builder.Services.AddSingleton(builder.Configuration); //Para todo ciclo de vida da aplicacao

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();
