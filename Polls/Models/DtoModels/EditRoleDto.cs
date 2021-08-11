using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.Models.DtoModels
{
    public class EditRoleDto
    {
        public Guid Id { get; set; }
        public String RoleName { get; set; }
    }
}
