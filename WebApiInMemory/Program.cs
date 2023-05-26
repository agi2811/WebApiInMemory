using Microsoft.EntityFrameworkCore;
using System;
using WebApiInMemory.Models;
using WebApiInMemory.Repository;
using WebApiInMemory.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
	var services = builder.Services;
	services.AddControllers();
	services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("ApplicationDb"));

	services.AddScoped<IEmployeeRepository, EmployeeRepository>();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();
	services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();
}

app.Run();
