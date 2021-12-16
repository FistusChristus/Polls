using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class CreatePollDto
    {
        public Poll Poll { get; set; }
        public IEnumerable<PollBackground> Backgrounds { get; set; }
    }
}
