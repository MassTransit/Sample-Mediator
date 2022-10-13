using MassTransit;
using Sample.ScopeApi;
using Sample.ScopeApi.Consumers;
using Sample.ScopeApi.Controllers;
using Sample.ScopeApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<WarehouseContext>();

builder.Services.AddMediator(x =>
{
    x.AddConsumersFromNamespaceContaining<Consumers>();

    x.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<WarehouseIdFilter>();

app.MapControllers();

app.Run();