using BurgerRoyale.API.ConfigController;
using BurgerRoyale.API.Middleware;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.IOC;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc(options =>
{
	options.Filters.Add(new CustomValidationModel());
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Burger Royale",
		Version = "v1"
	});

	options.IncludeXmlComments
	(
		Path.Combine
		(
			AppContext.BaseDirectory,
			$"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
		)
	);

	options.EnableAnnotations();
});

DependencyInjectionConfiguration.Register(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
