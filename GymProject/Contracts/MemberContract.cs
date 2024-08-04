namespace GymProject.Contracts;

public record class MemberContract(

    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    int Height,
    int Weight,
    string Reason,
    DateOnly JoiningDate
);
