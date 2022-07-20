namespace KidsRUs.Application.Models.ViewModels;

public class UserVm : IMapFrom<User>
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserVm>();
    }
}