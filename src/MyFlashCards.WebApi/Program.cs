using MyFlashCards.WebApi.Endpoints;
using MyFlashCards.WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddCors(options => options.AddPolicy("*", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

services.AddEndpoints(typeof(CardsEndpoint));

services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() {Title = "MyFlashCards.WebApi", Version = "v1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFlashCards.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseCors("*");

app.UseCustomExceptionHandling();

app.UseEndpoints();

app.Run();
