using API_Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace API_Core.DBContext
{
    public class TicketDbContext: DbContext
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

    }
}
