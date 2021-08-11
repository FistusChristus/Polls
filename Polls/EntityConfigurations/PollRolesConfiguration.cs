using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polls.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polls.EntityConfigurations
{
    public class PollRolesConfiguration : IEntityTypeConfiguration<PollsRole>
    {
        public void Configure(EntityTypeBuilder<PollsRole> builder)
        {
            builder.HasData(new PollsRole[]
            {
                new PollsRole{Id = new Guid("98eac94a-81b0-4202-b8b2-9aef6af98821"), ConcurrencyStamp = "a1e54ce4-b1bc-4081-864e-f35af2916a7c", Name = "Admin", NormalizedName = "ADMIN"},
                new PollsRole{Id = new Guid("fe23734f-f062-41bc-982b-8840d35c3754"), ConcurrencyStamp = "ffd15df0-993f-4d25-8774-7d846481c928", Name = "User", NormalizedName = "USER"}
            });
        }
    }
}
