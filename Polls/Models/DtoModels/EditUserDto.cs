using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class EditUserDto
    {
            public Guid Id { get; set; }
            public string Login { get; set; }
            public string Email { get; set; }
    }
}
