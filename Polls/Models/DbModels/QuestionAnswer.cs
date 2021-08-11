using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class QuestionAnswer
    {
        public Guid Id { get; set; }
        public string AnswerText { get; set; }

        public Guid PollQuestionId { get; set; }
        public PollQuestion PollQuestion { get; set; }
    }
}
