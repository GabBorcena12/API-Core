using API_Core.DBContext;
using API_Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public class TicketData : iTicket
    {
        private TicketDbContext _db;
        public TicketData(TicketDbContext db)
        {
            _db = db;
        }

        public int ModifyTicket(Ticket model)
        {

            Ticket ticket = new Ticket();

            if (model.Id <= 0)
            {
                ticket.Id = model.Id;
                ticket.Description = model.Description;
                ticket.TotalSeats = model.TotalSeats;
                ticket.AvailableSeats = model.AvailableSeats;
                ticket.DateCreated = DateTime.Now;
                ticket.StreamingDateTime = model.StreamingDateTime;


                _db.tblTicket.Add(ticket);
                _db.SaveChanges();

                var query = _db.tblTicket.Where(t => t.StreamingDateTime == ticket.StreamingDateTime && t.Description == ticket.Description).FirstOrDefault();
             
                if (query != null) {
                    return query.Id;
                }
                return 0;
            }
            else {

                var modelUpdate = _db.tblTicket.Find(model.Id);
                if (modelUpdate != null) {
                    Ticket insertModel = new Ticket();

                    insertModel.Id = model.Id;
                    insertModel.Description = (model.Description != null ? model.Description : modelUpdate.Description);
                    insertModel.TotalSeats = (model.TotalSeats > 0 ? model.TotalSeats : modelUpdate.TotalSeats);
                    insertModel.DateCreated = (model.DateCreated != null ? model.DateCreated : modelUpdate.DateCreated);
                    insertModel.StreamingDateTime = (model.StreamingDateTime != null ? model.StreamingDateTime : modelUpdate.StreamingDateTime);
                    insertModel.AvailableSeats = (model.AvailableSeats > 0 ? model.AvailableSeats : modelUpdate.AvailableSeats); 

                    _db.Entry<Ticket>(modelUpdate).State = EntityState.Detached;
                    _db.tblTicket.Update(insertModel);
                    _db.SaveChanges();

                    return model.Id;
                }
            }
            return 0;

        }

        public List<Ticket> GetTicket()
        {
            return _db.tblTicket.ToList();
        }
        public Ticket GetTicketById(int id)
        {
            var query = _db.tblTicket.Find(id);
            return query;
        }
         
        public int DeleteTicketById(Ticket model)
        {   
            _db.tblTicket.Remove(model);
            _db.SaveChanges();
            return 1;
        }
    }
}
