using BurgerRoyale.API.ConfigController;
using BurgerRoyale.API.Middleware;
using BurgerRoyale.IOC;
using BurgerRoyale.IOC.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
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
builder.Services.AddHttpContextAccessor();

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

ConfigureDatabase.RunMigrations(app);

app.Run();
