using System.Diagnostics.Contracts;
using GymProject.Contracts;
using GymProject.Data;
using GymProject.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GymProject");
builder.Services.AddSqlite<GymContext>(connectionString);


var app = builder.Build();

app.MapMemberEndPoints();

await app.MigrateDatabaseAsync();

app.Run();
