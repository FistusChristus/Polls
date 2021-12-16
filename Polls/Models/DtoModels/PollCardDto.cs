using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class PollCardDto
    {
        public Poll Poll { get; set; }
        public IEnumerable<PollQuestion> PollQuestions { get; set; }
        public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
