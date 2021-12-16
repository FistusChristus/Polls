using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class UserAnswer
    {
        public Guid UserId { get; set; }
        public PollsUser User { get; set; }
        public Guid QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
        public Guid PollId { get; set; }
    }
}
