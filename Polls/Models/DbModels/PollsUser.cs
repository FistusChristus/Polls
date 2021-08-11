using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class PollsUser : IdentityUser<Guid>
    {
        public string NickName { get; set; }
        public ICollection<UserPoll> UserPolls { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<PollsUser> PollsUsers { get; set; }
    }
}
