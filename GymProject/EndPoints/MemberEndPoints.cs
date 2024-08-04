using GymProject.Contracts;
using GymProject.Data;
using GymProject.DataModelEntities;
using GymProject.Mapping;
using Microsoft.EntityFrameworkCore;
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
        group.MapGet("/", (GymContext dbContext) =>
         dbContext.Members
         .Include(member => member.Reason)
         .Select(member => member.ToContract()).AsNoTracking());



        // GET /members/1
        group.MapGet("/{id}", (int id, GymContext dbContext) =>
        {
            Member? member = dbContext.Members.Find(id);

            return member is null ?
            Results.NotFound() : Results.Ok(member.ToMemberContractDetails());
        })
        .WithName(GetMember);



        // POST /members
        group.MapPost("/", (CreateMemberContract newMember, GymContext dbContext) =>
        {
            Member member = newMember.ToEntity(dbContext);
            //member.Reason = dbContext.Reasons.Find(newMember.ReasonId);


            dbContext.Members.Add(member);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetMember,
                new { id = member.Id },
                member.ToMemberContractDetails());

        });



        // PUT(update) /members/1
        group.MapPut("/{id}", (int id, UpdateMemberContract updatedMember, GymContext dbContext) =>
        {

            //var index = contracts.FindIndex(member => member.Id == id);

            var existingMember = dbContext.Members.Find(id);

            if (existingMember is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingMember).CurrentValues.SetValues(updatedMember.ToEntity(dbContext, id));

            dbContext.SaveChanges();

            return Results.NoContent();

        });


        // DELETE /members/1
        group.MapDelete("/{id}", (int id, GymContext dbContext) =>
        {
            dbContext.Members
            .Where(member => member.Id == id)
            .ExecuteDelete();

            return Results.NoContent();
        });
        return group;
    }
}
