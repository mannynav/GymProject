using System.Diagnostics.Contracts;
using GymProject.Contracts;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


List<MemberContract> contracts = [

    new (
        1,
        "John",
        "Doe",
        "johndoe@gmail.com",
        "1234567890",
        180,
        80,
        "Lose Weight",
        new DateOnly(2021, 10, 10)
    ),
    new (
        2,
        "Beth",
        "Doe",
        "bethdoe@gmail.com",
        "0987654321",
        120,
        60,
        "Add Weight",
        new DateOnly(2022, 10, 10)
    )

];

app.MapGet("members", () => contracts);


app.MapGet("/", () => "Hello World!");

app.Run();
