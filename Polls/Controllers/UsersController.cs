using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Polls.Models;
using Polls.Models.DtoModels;
using Polls.Models.DbModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Polls.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        UserManager<PollsUser> _userManager;

        public UsersController(UserManager<PollsUser> userManager)
        {
            _userManager = userManager;
        }

        //public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                PollsUser user = new PollsUser {Id = model.Id, Email = model.Email, UserName = model.Login};
                var result = await _userManager.CreateAsync(user, model.Password);
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
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            PollsUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            EditUserDto model = new EditUserDto { Id = user.Id, Email = user.Email, Login = user.Login,};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDto model)
        {
            if (ModelState.IsValid)
            {
                PollsUser user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Login = model.Login;

                    var result = await _userManager.UpdateAsync(user);
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

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            PollsUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}