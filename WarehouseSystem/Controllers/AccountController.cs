using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

namespace WarehouseSystem.Controllers
{
    
    public class AccountController : Controller
    {
        IAccountServices accountServices;

        IWarehouseServices WarehouseServices; 

        public AccountController(IAccountServices _accountServices,IWarehouseServices _warehouseServices) {
            accountServices = _accountServices;
            WarehouseServices = _warehouseServices;
        }
        public IActionResult CreateAccount()
        { VMSignUp vm=new VMSignUp();
            vm.Warehouses=WarehouseServices.loadall();
            return View("CreateAccount",vm);
        }
        public async Task<IActionResult> SignUP(VMSignUp up)
        {
            var result = await accountServices.CreateAccount(up.signup);

            ViewData["result"] = result;
            VMSignUp vm = new VMSignUp();
            vm.Warehouses = WarehouseServices.loadall();
            return View("CreateAccount",vm);
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public async Task<IActionResult> CheckPassward(SignIn signIn)
        {
            var result = await accountServices.Login(signIn);
            if (result.Succeeded)
            {
                var user = await accountServices.GetuserInfo(signIn.Email);
                TempData["Username"] = user.UserName;
                if (user.Active == true)
                {
                    return RedirectToAction("Index", "Welcome");
                }
                else
                {
                    ViewData["result"] = "Inactive Account,Please Contact Support";
                    return View("Login");
                }

            }
            else
            {
                ViewData["result"] = "Invalid Username or Password";
                return View("Login");
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await accountServices.Logout();
            return View("Login");

        }
        public IActionResult NewRole()
        {
            return View("NewRole");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddRole(Role role)
        { 
            var result=await accountServices.AddRole(role);
            ViewData["result"] = result;
            return View("NewRole");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList(int pg = 1)
        {
            List<UsersDTO> users =await accountServices.Userlist();
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = users.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;

                return View("UserList", data);
            
        }
       [Authorize(Roles = "Admin")]
        public IActionResult RoleList(int pg = 1)
        {
            List<Role>roles= accountServices.Rolelist();
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = roles.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = roles.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("RoleList",data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserRoles(string Name, string UserId)
		{
			ViewData["Name"] = Name;

			List<UserRoles> userRoles = await accountServices.getRoles(UserId);

			return View(userRoles);
		}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(List<UserRoles> userRoles)
		{
			await accountServices.UpdateUserRoles(userRoles);
			userRoles = await accountServices.getRoles(userRoles[0].UserId);
			return View("UserRoles", userRoles);
		}
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Updated(string UserId, int pg = 1)
        {
           await accountServices.Update(UserId);
            List<UsersDTO> users =await accountServices.Userlist();
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = users.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;

            
            return View("UserList", data);
        }

	}
}