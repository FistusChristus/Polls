using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DbModels
{
    public class PollBackground
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LinkPass { get; set; }

        public ICollection<Poll> Polls { get; set; }
    }
}
