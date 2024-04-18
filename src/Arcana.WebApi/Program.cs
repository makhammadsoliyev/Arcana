using Arcana.DataAccess.Context;
using Arcana.DataAccess.UnitOfWorks;
using Arcana.Service.Helpers;
using Arcana.Service.Services.Assets;
using Arcana.Service.Services.Roles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ArcanaDbContext>(option
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IRoleService, RoleService>();

EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
