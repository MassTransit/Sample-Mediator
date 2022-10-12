using MassTransit;
using Sample.SagaApi.StateMachines;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediator(x =>
{
    // this demo is about mediator, not saga repositories, so just use in-memory
    x.SetInMemorySagaRepositoryProvider();

    x.AddSagaStateMachinesFromNamespaceContaining<StateMachines>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();