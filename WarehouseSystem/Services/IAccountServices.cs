using Microsoft.AspNetCore.Identity;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public interface IAccountServices
    {
        Task<ApplicationUsers> GetuserInfo(string username);
            Task<IdentityResult> CreateAccount(SignUp signUp);
        Task<SignInResult> Login(SignIn signIn);
        Task Logout();
        Task<IdentityResult> AddRole(Role role);
        // List<UsersDTO> Userlist();
        Task<List<UsersDTO>> Userlist();
        List<Role> Rolelist();
        Task UpdateUserRoles(List<UserRoles> liUserRoles);
        Task<List<UserRoles>> getRoles(string UserId);
        Task Update(string UserId);
        Task<ApplicationUsers> GetuserInfoId(string UserId);


    }
}
