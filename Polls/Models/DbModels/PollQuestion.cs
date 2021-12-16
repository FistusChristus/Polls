using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class PollQuestion
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<QuestionAnswer> Answers { get; set; }

        public Guid PollId { get; set; }
        public Poll Poll { get; set; }
    }
}
