using KidsRUs.Application.Helper;
using KidsRUs.Application.Repositories.Common;
using KidsRUs.Domain.Common;
using KidsRUs.Domain.Entities;

namespace KidsRUs.Persistence.Seeds;

public static class DefaultUsers
{
    public static async Task SeedAsync(IUnitOfWork unitOfWork)
    {
        if (unitOfWork.User.GetAll().Any())
            return;
        
        PasswordHelper.CreatePasswordHash("editor*", out var newPasswordHashEditor, out var newPasswordSaltEditor);
        unitOfWork.User.Add(new User
        {
            RoleId = (int) RoleType.Editor,
            Email = "editor@mail.com",
            FullName = "Editor Editor",
            PasswordHash = newPasswordHashEditor,
            PasswordSalt = newPasswordSaltEditor
        });
        

        PasswordHelper.CreatePasswordHash("admin*", out var newPasswordHashAdmin, out var newPasswordSaltAdmin);
        unitOfWork.User.Add(new User
        {
            RoleId = (int) RoleType.Admin,
            Email = "admin@mail.com",
            FullName = "Admin Admin",
            PasswordHash = newPasswordHashAdmin,
            PasswordSalt = newPasswordSaltAdmin
        });

        await unitOfWork.SaveAsync();
    }
}