namespace GymProject.Contracts;

public record class MemberContractDetails
(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    int Height,
    int Weight,
    int ReasonId,
    DateOnly JoiningDate
);
