using Polls.Data;
using Polls.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PollsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<PollsController> _logger;
        public PollsController(ILogger<PollsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Polls()
        {
            return View(await _dbContext.Polls.AsNoTracking().ToArrayAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Poll poll)
        {
            await _dbContext.Polls.AddAsync(poll);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                Poll poll = await _dbContext.Polls.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (poll != null)
                    return View(poll);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Poll poll)
        {
            _dbContext.Polls.Update(poll);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                Poll poll = await _dbContext.Polls.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (poll != null)
                    return View(poll);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                Poll poll = await _dbContext.Polls.FirstOrDefaultAsync(p => p.Id == id);
                if (poll != null)
                {
                    _dbContext.Polls.Remove(poll);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
