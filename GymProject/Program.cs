using System.Diagnostics.Contracts;
using GymProject.Contracts;
using GymProject.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapMemberEndPoints();

app.Run();
