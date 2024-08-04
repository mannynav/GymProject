using System.ComponentModel.DataAnnotations;

namespace GymProject.Contracts;

public record class UpdateMemberContract(

    int Id,
    [Required][StringLength(50)] string FirstName,
    [Required][StringLength(50)] string LastName,
    [Required][StringLength(50)] string Email,
    [Required][StringLength(11)] string PhoneNumber,
    [Range(1, 500)] int Height,
    [Range(1, 500)] int Weight,
    //[Required][StringLength(25)] string Reason,
    int ReasonId,
    [Required] DateOnly JoiningDate
);
