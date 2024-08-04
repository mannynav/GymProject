using GymProject.Contracts;
using GymProject.Data;
using GymProject.DataModelEntities;
using GymProject.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GymProject.EndPoints;

public static class MemberEndPoints
{
    const string GetMember = "GetMember";

    public static RouteGroupBuilder MapMemberEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("members").WithParameterValidation();

        // GET /members
        group.MapGet("/", async (GymContext dbContext) =>
         await dbContext.Members
         .Include(member => member.Reason)
         .Select(member => member.ToContract())
         .AsNoTracking()
         .ToListAsync());


        // GET /members/1
        group.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            Member? member = await dbContext.Members.FindAsync(id);
            return member is null ? Results.NotFound() : Results.Ok(member.ToMemberContractDetails());
        })
        .WithName(GetMember);



        // POST /members
        group.MapPost("/", async (CreateMemberContract newMember, GymContext dbContext) =>
        {
            Member member = newMember.ToEntity(dbContext);

            dbContext.Members.Add(member);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetMember,
                new { id = member.Id },
                member.ToMemberContractDetails());
        });



        // PUT(update) /members/1
        group.MapPut("/{id}", async (int id, UpdateMemberContract updatedMember, GymContext dbContext) =>
        {

            //var index = contracts.FindIndex(member => member.Id == id);

            var existingMember = await dbContext.Members.FindAsync(id);

            if (existingMember is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingMember).CurrentValues.SetValues(updatedMember.ToEntity(dbContext, id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();

        });


        // DELETE /members/1
        group.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            await dbContext.Members
            .Where(member => member.Id == id)
            .ExecuteDeleteAsync();

            return Results.NoContent();
        });
        return group;
    }
}
