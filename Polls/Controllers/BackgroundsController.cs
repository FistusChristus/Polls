using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polls.Data;
using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Controllers
{
    public class BackgroundsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<BackgroundsController> _logger;
        public BackgroundsController(ILogger<BackgroundsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.PollBackgrounds.AsNoTracking().ToArrayAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PollBackground background)
        {
            await _dbContext.PollBackgrounds.AddAsync(background);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                PollBackground background = await _dbContext.PollBackgrounds.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (background != null)
                    return View(background);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PollBackground background)
        {
            _dbContext.PollBackgrounds.Update(background);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                PollBackground background = await _dbContext.PollBackgrounds.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (background != null)
                    return View(background);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                PollBackground background = await _dbContext.PollBackgrounds.FirstOrDefaultAsync(p => p.Id == id);
                if (background != null)
                {
                    _dbContext.PollBackgrounds.Remove(background);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
