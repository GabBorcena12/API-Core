using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Core.Repository
{
    public class PriceData : IPrice
    {
        private TicketDbContext _db;
        public PriceData(TicketDbContext db)
        {
            _db = db;
        }
        public int DeleteTicketPrice(TicketPrice model)
        {
            _db.tblTicketPrice.Remove(model);
            _db.SaveChanges();
            return 1;
        }

        public List<TicketPrice> GetTicketPrice()
        {
            var query = _db.tblTicketPrice.ToList();

            return query;
        }

        public TicketPrice GetTicketPriceById(int id)
        {
            var query = _db.tblTicketPrice.Find(id);

            return query;
        }

        public int ModifyTicketPrice(TicketPrice model)
        {
            if (model.Id > 0)
            {
                //update
                var modelUpdate = _db.tblTicketPrice.Find(model.Id);
                if (modelUpdate != null)
                {
                    TicketPrice insertModel = new TicketPrice();

                    insertModel.Id = model.Id;
                    insertModel.TicketId = model.TicketId > 0 ? model.TicketId : modelUpdate.TicketId;
                    insertModel.SeatId = model.SeatId > 0 ? model.SeatId : modelUpdate.SeatId;
                    insertModel.Price = model.Price > 0 ? model.Price : modelUpdate.Price;

                    _db.Entry(modelUpdate).State = EntityState.Detached;
                    _db.tblTicketPrice.Update(insertModel);
                    _db.SaveChanges();
                    return model.Id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //create
                TicketPrice price = new TicketPrice();
                price.Id = model.Id;
                price.Price = model.Price;
                price.SeatId = model.SeatId;
                price.TicketId = model.TicketId;

                _db.tblTicketPrice.Add(price);
                _db.SaveChanges();

                var query = _db.tblTicketPrice.Where(x => x.TicketId == model.TicketId && x.SeatId == model.SeatId &&
                x.Price == model.Price).FirstOrDefault();
                if (query.Id > 0)
                {
                    return query.Id;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
