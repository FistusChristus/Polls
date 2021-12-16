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
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<AnswersController> _logger;
        public AnswersController(ILogger<AnswersController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.QuestionAnswers.AsNoTracking().ToArrayAsync());
        }

        public IActionResult Create()
        {
            var createAnswerDto = new CreateAnswerDto();
            createAnswerDto.PollQuestions = _dbContext.PollQuestions.ToArray();
            return View(createAnswerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnswerDto answer)
        {
            var questionAnswer = new QuestionAnswer
            {
                Id = answer.QuestionAnswer.Id,
                PollQuestion = answer.QuestionAnswer.PollQuestion,
                PollId = answer.QuestionAnswer.PollId,
                PollQuestionId = answer.QuestionAnswer.PollQuestionId,
                Text = answer.QuestionAnswer.Text,
                UserAnswers = answer.QuestionAnswer.UserAnswers
            };

            await _dbContext.QuestionAnswers.AddAsync(questionAnswer);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                var createAnswerDto = new EditAnswerDto();
                createAnswerDto.PollQuestions = _dbContext.PollQuestions.ToArray();
                createAnswerDto.QuestionAnswer = await _dbContext.QuestionAnswers.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (createAnswerDto.QuestionAnswer != null)
                    return View(createAnswerDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAnswerDto answer)
        {
            var questionAnswer = new QuestionAnswer
            {
                Id = answer.QuestionAnswer.Id,
                PollQuestion = answer.QuestionAnswer.PollQuestion,
                PollQuestionId = answer.QuestionAnswer.PollQuestionId,
                Text = answer.QuestionAnswer.Text,
                UserAnswers = answer.QuestionAnswer.UserAnswers
            };

            _dbContext.QuestionAnswers.Update(questionAnswer);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                QuestionAnswer answer = await _dbContext.QuestionAnswers.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (answer != null)
                    return View(answer);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                QuestionAnswer answer = await _dbContext.QuestionAnswers.FirstOrDefaultAsync(p => p.Id == id);
                if (answer != null)
                {
                    _dbContext.QuestionAnswers.Remove(answer);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
