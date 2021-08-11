using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class RoleCreateDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
    }
}
