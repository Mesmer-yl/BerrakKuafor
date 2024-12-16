using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using KuaforSite.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class UserOperationsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserOperationsController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewUser(RegisterVM _registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newUser = new AppUser()
            {
                UserName = _registerVM.Email,
                Email = _registerVM.Email,
                NameSurname = _registerVM.NameSurname,
                PhoneNumber = _registerVM.PhoneNumber
            };
            var identityResult = await _userManager.CreateAsync(newUser, _registerVM.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Member");
                TempData["UpdateUserMessage"] = "Yeni kullanıcı başarıyla kaydedildi.";
                return RedirectToAction(nameof(UpdateUser), "UserOperations", new { userId = newUser.Id });
            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var userList = await _userManager.Users.ToListAsync();
            var userModelList = new List<UsersWithRoleVM>();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var user in userList)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var userModel = new UsersWithRoleVM()
                {
                    Id = user.Id,
                    NameSurname = user.NameSurname,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    RoleName = userRole.SingleOrDefault()
                };
                userModelList.Add(userModel);
            }
            return View(userModelList);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();
            var currentUserRole= await _userManager.GetRolesAsync(currentUser);
            var userModel = new UserUpdateByModVM()
            {
                Id = currentUser.Id,
                NameSurname = currentUser.NameSurname,
                PhoneNumber = currentUser.PhoneNumber,
                Email = currentUser.Email,
                RoleId = roles.FirstOrDefault(r => currentUserRole.Contains(r.Name))?.Id,
                Roles = roles.Select(role => new RoleItem
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList()
            };

            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateByModVM _userUpdateByModVM)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(_userUpdateByModVM.Id);
                currentUser.NameSurname=_userUpdateByModVM.NameSurname;
                currentUser.PhoneNumber =_userUpdateByModVM.PhoneNumber;
                currentUser.Email = _userUpdateByModVM.Email;
                var currentRoles = await _userManager.GetRolesAsync(currentUser);
                await _userManager.RemoveFromRolesAsync(currentUser, currentRoles);
                var selectedRole = await _roleManager.FindByIdAsync(_userUpdateByModVM.RoleId);
                if (selectedRole != null)
                {
                    await _userManager.AddToRoleAsync(currentUser, selectedRole.Name);
                }

                await _userManager.UpdateAsync(currentUser);
                TempData["UpdateUserMessage"] = "Kullanıcı bilgileri başarıyla güncellendi.";
                return RedirectToAction(nameof(UserOperationsController.UpdateUser),"UserOperations", new {userId = currentUser.Id}); 
            }

            var roleList = await _roleManager.Roles.ToListAsync();
            _userUpdateByModVM.Roles = roleList.Select(role => new RoleItem
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
            TempData["UpdateUserMessage"] = "Kullanıcı bilgileri güncellenemedi.";
            return View(_userUpdateByModVM);
        }
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            ViewBag.Roles = await GetAllRole();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Roles(RoleAddVM _roleAddVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole() { Name = _roleAddVM.Name });
                if (result.Succeeded)
                {
                    TempData["RoleAddMessage"] = "Rol başarıyla oluşturulmuştur";
                    return RedirectToAction(nameof(UserOperationsController.Roles), "UserOperations");
                }
                ViewBag.Roles = await GetAllRole();
                ModelState.AddModelErrorList(result.Errors.Select(x=>x.Description).ToList());
            }
            
            return View(_roleAddVM);
        }
        public async Task<List<RolesVM>> GetAllRole()
        {
            var roles = _roleManager.Roles.Select(x => new RolesVM()
            {
                Id = x.Id,
                Name = x.Name
            });
            return await roles.ToListAsync();
        }
    }
}
