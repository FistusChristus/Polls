using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class CreateQuestionDto
    {
        public PollQuestion PollQuestion { get; set; }
        public IEnumerable<Poll> Polls { get; set; }
    }
}
