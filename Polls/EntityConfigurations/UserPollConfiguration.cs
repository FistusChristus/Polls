using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.EntityConfigurations
{
    public class UserPollConfiguration : IEntityTypeConfiguration<UserPoll>
    {
        public void Configure(EntityTypeBuilder<UserPoll> builder)
        {
            builder.HasKey(bc => new { bc.PollId, bc.UserId });
        }
    }
}
