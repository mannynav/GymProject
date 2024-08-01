namespace GymProject.Contracts;

public record class CreateMemberContract(

    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    int Height,
    int Weight,
    string Goal,
    DateOnly JoiningDate
    
);