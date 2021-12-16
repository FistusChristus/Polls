using Polls.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Polls.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Polls.Models.DbModels;

namespace AstinOrg.ControllersWebApi
{
    [Route("/api/polls")]
    [ApiController]
    [Authorize]
    public class PollsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public PollsApiController(ApplicationDbContext context) => _dbContext = context;

        [HttpPost("set-userAnswers")]
        public async Task SetAll([FromBody] UserAnswersDto userAnswers)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userPoll = new UserPoll
            {
                PollId = userAnswers.PollId,
                UserId = userId
            };

            await _dbContext.UserPolls.AddAsync(userPoll);

            foreach(var answer in userAnswers.AnswerIds)
            {
                var userAnswer = new UserAnswer
                {
                    QuestionAnswerId = answer,
                    UserId = userId,
                    PollId = userAnswers.PollId
                };
                await _dbContext.UserAnswers.AddAsync(userAnswer);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}



