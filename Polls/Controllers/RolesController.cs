using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Polls.Models;
using Polls.Models.DtoModels;
using Polls.Models.DbModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Polls.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<PollsRole> _roleManager;
        UserManager<PollsUser> _userManager;
        public RolesController(RoleManager<PollsRole> roleManager, UserManager<PollsUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDto roleCreateDto)
        {
            if (!string.IsNullOrWhiteSpace(roleCreateDto.RoleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new PollsRole { Id = roleCreateDto.Id, Name = roleCreateDto.RoleName });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(roleCreateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            PollsRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            PollsUser pollsUser = await _userManager.FindByIdAsync(userId);
            if (pollsUser != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(pollsUser);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleDto model = new ChangeRoleDto
                {
                    UserId = pollsUser.Id.ToString(),
                    UserEmail = pollsUser.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            PollsUser pollsUser = await _userManager.FindByIdAsync(userId);
            if (pollsUser != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(pollsUser);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(pollsUser, addedRoles);

                await _userManager.RemoveFromRolesAsync(pollsUser, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        public async Task<IActionResult> EditRole(Guid id)
        {
            PollsRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }
            EditRoleDto model = new EditRoleDto { Id = role.Id, RoleName = role.Name };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleDto model)
        {
            if (ModelState.IsValid)
            {
                PollsRole role = await _roleManager.FindByIdAsync(model.Id.ToString());
                if (role != null)
                {
                    role.Name = model.RoleName;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}