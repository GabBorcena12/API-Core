using API_Core.Data;
using API_Core.Model.Models; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace API_Core.DBContext
{
    public class TicketDbContext: IdentityDbContext<ApiUser>
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options)
        {

        } 
        public DbSet<Ticket> tblTicket { get; set; } 
        public DbSet<ErrorDetails> tblErrorLogs { get; set; }
        public DbSet<SeatCategory> tblSeatCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
