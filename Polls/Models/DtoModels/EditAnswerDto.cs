using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class EditAnswerDto
    {
        public QuestionAnswer QuestionAnswer { get; set; }
        public IEnumerable<PollQuestion> PollQuestions { get; set; }
    }
}
