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
        public DbSet<Transact> tblTransaction { get; set; }
        public DbSet<Ticket> tblTicket { get; set; }
        public DbSet<TicketSeats> tblTicketSeats { get; set; }
        public DbSet<TicketPrice> tblTicketPrice { get; set; }
        public DbSet<TouristDestination> tblTouristDestinations { get; set; }
        public DbSet<ErrorDetails> tblErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
