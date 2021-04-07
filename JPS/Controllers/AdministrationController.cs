
using JPS.Models;
using JPS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_INDIVIDUAL_ASSIGNMENT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {

        private readonly RoleManager<IdentityRole> role_Manager;
        private readonly UserManager<JPSUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> _roleManager, UserManager<JPSUser> _userManager)
        {
            this.role_Manager = _roleManager;
            this.userManager = _userManager;
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.role_name
                };

                IdentityResult result = await role_Manager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {

            ViewBag.roleId = roleId;

            var role = await role_Manager.FindByNameAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} connot be found";

                return View("Not Found");
            }

            var model = new List<UserRoleManagerModel>();

            foreach (var user in userManager.Users)
            {
                var UserRoleManagerModel = new UserRoleManagerModel
                {
                    UserId = user.Id,
                    username = user.UserName
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoleManagerModel.IsSelected = true;
                }
                else
                {
                    UserRoleManagerModel.IsSelected = false;
                }

                model.Add(UserRoleManagerModel);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(JPSUser model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if(user != null)
            {
                user.UserName = model.UserName;

                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("ListUsers");
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = role_Manager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await role_Manager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {role} cannot be found";

                return View("Not Found");
            }

            var model = new EditRole
            {
                id = role.Id,
                role_name = role.Name
            };

            foreach(var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRole model)
        {
            var role = await role_Manager.FindByIdAsync(model.id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.id} cannot be found";

                return View("Not Found");

            }else
            {
                role.Name = model.role_name;
                var result = await role_Manager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditRole model)
        {
            var role = await role_Manager.FindByIdAsync(model.id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.id} cannot be found";

                return View("Not Found");

            }
            else
            {
                role.Name = model.role_name;

                var result = await role_Manager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
                return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await role_Manager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleManagerModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleManagerModel
                {
                    UserId = user.Id,
                    username = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddUserToRole(List<UserRoleManagerModel> model, string roleId)
        {
            var role = await role_Manager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
