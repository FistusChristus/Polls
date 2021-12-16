using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Polls.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Polls.EntityConfigurations;

namespace Polls.Data
{
    public class ApplicationDbContext : IdentityDbContext<PollsUser, PollsRole, Guid>
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollsUser> PollsUsers { get; set; }
        public DbSet<PollsRole> PollsRoles { get; set; }
        public DbSet<UserPoll> UserPolls { get; set; }
        public DbSet<PollQuestion> PollQuestions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<PollBackground> PollBackgrounds { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserPollConfiguration());
            modelBuilder.ApplyConfiguration(new UserAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new PollRolesConfiguration());
        }
    }
}

