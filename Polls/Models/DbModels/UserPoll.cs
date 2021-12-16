using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class UserPoll
    {
        public Guid PollId { get; set; }
        public Poll Poll { get; set; }
        public Guid UserId { get; set; }
        public PollsUser User { get; set; }
    }
}
