using Microsoft.AspNetCore.Identity;
using Polls.Models.DbModels;
using System.Collections.Generic;
 
namespace Polls.Models.DtoModels {
    public class ChangeRoleDto
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<PollsRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleDto()
        {
            AllRoles = new List<PollsRole>();
            UserRoles = new List<string>();
        }
    }
}
