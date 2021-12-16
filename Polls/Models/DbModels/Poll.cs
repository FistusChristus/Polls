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

        public ICollection<PollQuestion> PollQuestions { get; set; }

        public ICollection<UserPoll> UserPolls { get; set; }

        public Guid? CreatorId { get; set; }
        public PollsUser Creator { get; set; }

        public Guid BackgroundId { get; set; }
        public PollBackground Background { get; set; }
    }
}
