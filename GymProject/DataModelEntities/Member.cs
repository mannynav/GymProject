using GymProject.Entites;

namespace GymProject;

public class Member
{

    public int AimId { get; set; }
    public Aims? Aims { get; set; }


    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }

    public string? Goal { get; set; }

    public DateOnly JoiningDate { get; set; }

}
