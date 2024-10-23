using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application;
using Application.Commands;
using Application.Queries;
using BuildingBlocks;
using Contractor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Repositories;
using Services;
using Services.ApplicationEvent.Tool;
using SharedIdentity;
using Tender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssemblies([typeof(IServiceFlag).Assembly]);
});

builder.Services.AddTransient<IAggregateEventProvider, MediatrAggregateEventProvider>();
builder.Services.AddTransient<ITenderRepository, TenderRepository>();
builder.Services.AddTransient<IContractorRepository, ContractorRepository>();

var connectionStringBuilder = builder.Configuration
    .GetSection("SqlServerConfig")
    .Get<SqlConnectionStringBuilder>();

builder.Services
    .AddDbContextPool<PhoenixDbContext>(o
        => o.UseSqlServer(connectionStringBuilder!.ToString()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();


app.MapGet("Tender/Get",
    async (ISender sender, [FromQuery] long tenderId) => await sender.Send(new GetTenderQuery(tenderId)));

app.MapGet("Contractor/Get",
    async (ISender sender, [FromQuery] long contractorId) => await sender.Send(new GetContractorQuery(contractorId)));

app.MapPost("Tender/Get",
    async (ISender sender, [FromBody] GetAllTendersQuery query) => await sender.Send(query));

app.MapPost("Contractor/Get",
    async (ISender sender, [FromBody] GetAllContractorQuery query) => await sender.Send(query));

app.MapPost("Tender/OpenTender",
    async (ISender sender, [FromBody] OpenTenderCommand command) => await sender.Send(command));

app.MapPost("Contractor/Add",
    async (ISender sender, [FromBody] AddContractorCommand command) => await sender.Send(command));

app.MapPut("Tender/BidToTender",
    async (ISender sender, [FromBody] BidToTenderCommand command) => await sender.Send(command));

app.MapPut("Tender/CloseTender",
    async (ISender sender, [FromBody] CloseTenderCommand command) => await sender.Send(command));

app.MapPut("Tender/CancelTender",
    async (ISender sender, [FromBody] CancelTenderCommand command) => await sender.Send(command));

app.Run();