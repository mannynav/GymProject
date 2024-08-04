using GymProject.Contracts;
using GymProject.Data;
using GymProject.DataModelEntities;

namespace GymProject.Mapping;

public static class MemberMapping
{

    public static Member ToEntity(this CreateMemberContract newMember, GymContext dbContext)
    {
        return new Member()
        {
            FirstName = newMember.FirstName,
            LastName = newMember.LastName,
            Email = newMember.Email,
            PhoneNumber = newMember.PhoneNumber,
            Height = newMember.Height,
            Weight = newMember.Weight,
            ReasonId = newMember.ReasonId,
            Goal = dbContext.Reasons.Find(newMember.ReasonId)!.NameOfReason,
            JoiningDate = newMember.JoiningDate
        };
    }

    public static MemberContract ToContract(this Member member)
    {
        return new(
            member.Id,
            member.FirstName,
            member.LastName,
            member.Email,
            member.PhoneNumber,
            member.Height,
            member.Weight,
            member.Reason!.NameOfReason,
            member.JoiningDate
        );
    }




}
