using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class Poll
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }

        public List<PollQuestion> PollQuestions { get; set; }
        public ICollection<UserPoll> UserPolls { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public Guid UserId { get; set; }
        public PollsUser PollsUser { get; set; }
    }
}
