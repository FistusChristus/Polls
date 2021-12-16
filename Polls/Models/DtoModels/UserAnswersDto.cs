using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class UserAnswersDto
    {
         public Guid PollId { get; set; }
         public ICollection<Guid> AnswerIds { get; set; }
    }
}
