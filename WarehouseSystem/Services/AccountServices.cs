using Microsoft.AspNetCore.Identity;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public class AccountServices:IAccountServices
    {
        UserManager<ApplicationUsers> userManager;
        SignInManager<ApplicationUsers> SignInManager;
         RoleManager<IdentityRole> roleManager;

        public AccountServices(UserManager<ApplicationUsers> _userManager,
            SignInManager<ApplicationUsers> _signInManager,RoleManager<IdentityRole> _roleManager) {
            userManager = _userManager;
            SignInManager = _signInManager;
            roleManager = _roleManager;
        }


        public async Task<IdentityResult> CreateAccount(SignUp signUp)
        {
            ApplicationUsers user = new ApplicationUsers()
            {
                FullName = signUp.FullName,
                Active = true,
                UserName = signUp.Email,
                Warehouse_Id = signUp.Warehouse_Id,
            };

            var result = await userManager.CreateAsync(user, signUp.Password);
            return result;
        }
        public async Task<SignInResult> Login(SignIn signIn)
        {
            var result = await SignInManager.PasswordSignInAsync (signIn.Email, signIn.Password, signIn.remmberme, false);
            return result;
        }
        public async Task<ApplicationUsers> GetuserInfo(string username)
        {
           var user=await userManager.FindByNameAsync(username);

            return user;
        }
        public async Task<ApplicationUsers> GetuserInfoId(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            return user;
        }
        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }
        public async Task<IdentityResult> AddRole(Role role)
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Name =role.Name,
            };
            var result = await roleManager.CreateAsync(identityRole);
            return result;
        }

        public async Task<List<UsersDTO>> Userlist()
        { List<UsersDTO> users=new List<UsersDTO>();
            List<ApplicationUsers> applicationUsers= userManager.Users.ToList();
            foreach (var user in applicationUsers)
            {

                users.Add(new UsersDTO()
                {
                    FullName= user.FullName,
                    userId= user.Id,
                     Email= user.UserName,
                     Active=user.Active,
                    Warehouse_Id=user.Warehouse_Id,

                });

            }
            return users;
        }

        public List<Role> Rolelist()
        {
            List<Role> role = new List<Role>();
            List<IdentityRole> identityRoles=roleManager.Roles.ToList();
            foreach(IdentityRole role1 in identityRoles)
            {
                role.Add(new Role()
                {
                    Name=role1.Name,
                });

            }
            return role;
        }
		public async Task<List<UserRoles>> getRoles(string UserId)
		{
			List<Role> roles = new List<Role>();
			List<IdentityRole> li = roleManager.Roles.ToList();

			List<UserRoles> userRoles = new List<UserRoles>();


			foreach (var item in li)
			{

				userRoles.Add(new UserRoles()
				{
					RoleId = item.Id,
					RoleName = item.Name,
					IsSelected = false,
					UserId = UserId
				});

			}

			foreach (var item in userRoles)
			{
				var user = await userManager.FindByIdAsync(UserId);
				var uroles = await userManager.GetRolesAsync(user);
				foreach (var roleName in uroles)
				{
					if (roleName == item.RoleName)
					{
						item.IsSelected = true;
					}
				}

			}

			return userRoles;
		}
		public async Task UpdateUserRoles(List<UserRoles> liUserRoles)
		{

			foreach (var item in liUserRoles)
			{
				var user = await userManager.FindByIdAsync(item.UserId);
				if (item.IsSelected)
				{
					if (await userManager.IsInRoleAsync(user, item.RoleName) == false)
						await userManager.AddToRoleAsync(user, item.RoleName);
				}
				else
				{
					if (await userManager.IsInRoleAsync(user, item.RoleName))
					{
						await userManager.RemoveFromRoleAsync(user, item.RoleName);
					}
				}
			}
		}

        public async Task Update(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user.Active == true)
            {
                user.Active= false;
            }
            else
            {
                user.Active= true;
            }
          await userManager.UpdateAsync(user);

        }


    }
}
