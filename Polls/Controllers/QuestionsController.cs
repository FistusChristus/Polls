using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polls.Data;
using Polls.Models.DbModels;
using Polls.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<QuestionsController> _logger;
        public QuestionsController(ILogger<QuestionsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.PollQuestions.AsNoTracking().ToArrayAsync());
        }

        public IActionResult Create()
        {
            var createQuestionDto = new CreateQuestionDto();
            createQuestionDto.Polls = _dbContext.Polls.ToArray();
            return View(createQuestionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionDto question)
        {
            var pollQuestion = new PollQuestion
            {
                Id = question.PollQuestion.Id,
                Answers = question.PollQuestion.Answers,
                Poll = question.PollQuestion.Poll,
                PollId = question.PollQuestion.PollId,
                Text = question.PollQuestion.Text
            };
            await _dbContext.PollQuestions.AddAsync(pollQuestion);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                var createQuestionDto = new EditQuestionDto();
                createQuestionDto.Polls = _dbContext.Polls.ToArray();
                createQuestionDto.PollQuestion = await _dbContext.PollQuestions.AsNoTracking().Include(p => p.Answers).FirstOrDefaultAsync(p => p.Id == id);
                if (createQuestionDto.PollQuestion != null)
                    return View(createQuestionDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditQuestionDto question)
        {
            var pollQuestion = new PollQuestion
            {
                Id = question.PollQuestion.Id,
                Answers = question.PollQuestion.Answers,
                Poll = question.PollQuestion.Poll,
                PollId = question.PollQuestion.PollId,
                Text = question.PollQuestion.Text
            };
            _dbContext.PollQuestions.Update(pollQuestion);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                PollQuestion question = await _dbContext.PollQuestions.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (question != null)
                    return View(question);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                PollQuestion question = await _dbContext.PollQuestions.FirstOrDefaultAsync(p => p.Id == id);
                if (question != null)
                {
                    _dbContext.PollQuestions.Remove(question);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
