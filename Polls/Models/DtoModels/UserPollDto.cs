using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class UserPollDto
    {
        public Poll Poll { get; set; }
        public IEnumerable<Guid> UserAnswerIds { get; set; }
        public Dictionary<Guid, int> answerVotes = new Dictionary<Guid, int>(5);
    }
}
