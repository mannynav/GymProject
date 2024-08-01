using System.Diagnostics.Contracts;
using GymProject.Contracts;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetMember = "GetMember";


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


// GET /members
app.MapGet("members", () => contracts);

// GET /members/1
app.MapGet("members/{id}", (int id) => contracts.Find(member => member.Id == id))
.WithName(GetMember);

// GET /members/lastName
//app.MapGet("members/{lastName}",(string lastName) => contracts.Find(member => member.LastName == lastName));

// GET /members/phoneNumber
//app.MapGet("members/{phoneNumber}",(string phoneNumber) => contracts.Find(member => member.PhoneNumber == phoneNumber))
//.WithName(GetMember);



// POST /members
app.MapPost("members", (CreateMemberContract contract) =>
{
    MemberContract newMember = new MemberContract(
        contracts.Count + 1,
        contract.FirstName,
        contract.LastName,
        contract.Email,
        contract.PhoneNumber,
        contract.Height,
        contract.Weight,
        contract.Goal,
        contract.JoiningDate
    );

    contracts.Add(newMember);

    return Results.CreatedAtRoute(GetMember, new { id = newMember.Id }, newMember);

});


app.Run();
