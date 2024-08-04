using GymProject.Contracts;
using GymProject.Data;
using GymProject.DataModelEntities;
namespace GymProject.EndPoints;

public static class MemberEndPoints
{
    const string GetMember = "GetMember";

    private static readonly List<MemberContract> contracts = [

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

    public static RouteGroupBuilder MapMemberEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("members").WithParameterValidation();

        // GET /members
        group.MapGet("/", () => contracts);

        // GET /members/1
        group.MapGet("/{id}", (int id) =>
        {
            MemberContract? member = contracts.Find(member => member.Id == id);

            return member is null ? Results.NotFound() : Results.Ok(member);
        }
        )
        .WithName(GetMember);

        // GET /members/lastName
        // app.MapGet("members/{lastName}",(string lastName) => contracts.Find(member => member.LastName == lastName));

        // GET /members/phoneNumber
        // app.MapGet("members/{phoneNumber}",(string phoneNumber) => contracts.Find(member => member.PhoneNumber == phoneNumber))
        //.WithName(GetMember);



        // POST /members
        group.MapPost("/", (CreateMemberContract newMember, GymContext dbContext) =>
        {
            Member member = new()
            {
                FirstName = newMember.FirstName,
                LastName = newMember.LastName,
                Email = newMember.Email,
                PhoneNumber = newMember.PhoneNumber,
                Height = newMember.Height,
                Weight = newMember.Weight,
                ReasonId = newMember.ReasonId,
                Reason = dbContext.Reasons.Find(newMember.ReasonId),
                JoiningDate = newMember.JoiningDate
            };

            dbContext.Members.Add(member);

            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetMember, new { id = member.Id }, member);

        });



        // PUT(update) /members/1
        group.MapPut("/{id}", (int id, UpdateMemberContract updatedMember) =>
        {

            var index = contracts.FindIndex(member => member.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            contracts[index] = new MemberContract(
                id,
                updatedMember.FirstName,
                updatedMember.LastName,
                updatedMember.Email,
                updatedMember.PhoneNumber,
                updatedMember.Height,
                updatedMember.Weight,
                updatedMember.Reason,
                updatedMember.JoiningDate
            );

            return Results.NoContent();

        });


        // DELETE /members/1
        group.MapDelete("/{id}", (int id) =>
        {
            contracts.RemoveAll(member => member.Id == id);
            return Results.NoContent();

        });



        return group;

    }

}
