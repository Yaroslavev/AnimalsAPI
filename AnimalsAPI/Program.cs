using AnimalsAPI;
using Core.IServices;
using Core.MapperProfiles;
using Core.Services;
using Data;
using Data.Enteties;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimalsDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(AppProfile));

builder.Services.AddIdentity<User, IdentityRole>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AnimalsDbContext>();

builder.Services.AddScoped<IAccountsService, AccountsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
