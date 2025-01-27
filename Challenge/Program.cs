using Challenge.Data;
using Challenge.Repository.imp;
using Challenge.Repository;
using Challenge.Service.Imp;
using Challenge.Service;
using Challenge.Validator;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MedicalContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepositoryImp>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordServiceImp>();
builder.Services.AddScoped<IStatusRepository, StatusRepositoryImp>();
builder.Services.AddScoped<IMedicalRecordTypeRepository, MedicalRecordTypeRepositoryImp>();
builder.Services.AddValidatorsFromAssemblyContaining<MedicalRecordValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateValidator>();

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
