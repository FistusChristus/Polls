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
using System.Security.Claims;
using Polls.Models.DtoModels;

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

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Polls.AsNoTracking().ToArrayAsync());
        }

        public async Task<IActionResult> Poll(Guid id)
        { 
            var userPollDto = new UserPollDto();
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userPoll = await _dbContext.UserPolls.AsNoTracking().FirstOrDefaultAsync(up => up.UserId == userId && up.PollId == id);
            if (userPoll != null)
            {
                userPollDto.UserAnswerIds = await _dbContext.UserAnswers.AsNoTracking().Where(up => up.UserId == userId && up.PollId == id)
                    .Select(ua => ua.QuestionAnswerId).ToArrayAsync(); 
            }
           
            foreach (var answer in await _dbContext.QuestionAnswers.ToArrayAsync())
            {
                var count = await _dbContext.UserAnswers.AsNoTracking().Where(ua => ua.QuestionAnswerId == answer.Id).CountAsync();
                userPollDto.answerVotes.Add(answer.Id, count);
            }
            userPollDto.Poll = await _dbContext.Polls.AsNoTracking().Include(p => p.PollQuestions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(p => p.Id == id);
            return View(userPollDto);
        }

        [HttpPost]
        public async Task<IActionResult> Poll(Poll poll)
        {
            _dbContext.Polls.Update(poll);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var createPollDto = new CreatePollDto();
            createPollDto.Backgrounds = _dbContext.PollBackgrounds.ToArray();
            return View(createPollDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePollDto createPoll)
        {
            var poll = new Poll
            {
                Background = createPoll.Poll.Background,
                BackgroundId = createPoll.Poll.BackgroundId,
                Creator = createPoll.Poll.Creator,
                CreatorId = createPoll.Poll.CreatorId,
                Id = createPoll.Poll.Id,
                Name = createPoll.Poll.Name,
                PollQuestions = createPoll.Poll.PollQuestions,
                UserPolls = createPoll.Poll.UserPolls
            };
            poll.CreatorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _dbContext.Polls.AddAsync(poll);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                var editPollDto = new EditPollDto();
                editPollDto.Backgrounds = _dbContext.PollBackgrounds.ToArray();
                editPollDto.Poll = await _dbContext.Polls.AsNoTracking().Include(p => p.PollQuestions).FirstOrDefaultAsync(p => p.Id == id);
                var userPolls = await _dbContext.UserPolls.AsNoTracking().Where(pi => pi.PollId == id).Select(ui => ui.UserId).ToArrayAsync();
                foreach(var user in userPolls)
                editPollDto.UserNames = _dbContext.PollsUsers.AsNoTracking().Where(ui => ui.Id == user).Select(un => un.Login);
                if (editPollDto.Poll != null && editPollDto.UserNames != null)
                    return View(editPollDto);
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
