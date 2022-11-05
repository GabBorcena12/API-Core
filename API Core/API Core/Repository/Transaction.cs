using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API_Core.Repository
{
    public class Transaction : iTransaction
    {
        private TicketDbContext _db;
        private TicketData _ticket;
        private SeatData _seat;
        public Transaction(TicketDbContext db, TicketData iTicket, SeatData seat)
        {
            _db = db;
            _ticket = iTicket;
            _seat = seat;
        }

        public int DeleteTransaction(int id)
        {
            var result = _db.tblTransaction.Find(id);
            if (result != null)
            {
                _db.Remove(result);
                return 1;
            }

            return 0;
        }

        public List<Transact> GetTransactions()
        {
            return _db.tblTransaction.ToList();

        }

        public Transact GetTransactionsById(int id)
        {
            var result = _db.tblTransaction.Find(id);
            return result;
        }

        public int ModifyTransaction(Transact model)
        {
            if (model.Id <= 0)
            {
                Transact transact = new Transact();

                transact.Id = model.Id;
                transact.TicketId = model.TicketId;
                transact.AcquiredSeats = model.AcquiredSeats;
                transact.StreamingDateTime = model.StreamingDateTime;
                transact.SeatId = model.SeatId;
                transact.PriceId = model.PriceId;

                var checkAvailability = _seat.CheckAvailableSeat(transact.SeatId, transact.AcquiredSeats, transact.Id, false);
                if (checkAvailability > 0)
                {
                    _db.tblTransaction.Add(transact);
                    _db.SaveChanges();



                    var query = _db.tblTransaction.Where(x => x.TicketId == model.TicketId && x.AcquiredSeats == model.AcquiredSeats &&
                    x.StreamingDateTime == model.StreamingDateTime).FirstOrDefault();


                    if (query != null)
                    {
                        //Find Ticket   

                        var findTicket = _ticket.GetAsyncId(model.TicketId);

                        if (findTicket != null)
                        {

                            //Create/Update Ticket
                            if (model.Id <= 0)
                            {
                                CreateTicketDto updateTIcket = new CreateTicketDto();

                                updateTIcket.Id = model.TicketId;
                                updateTIcket.Description = findTicket.Description;
                                updateTIcket.TotalSeats = findTicket.TotalSeats;
                                updateTIcket.AvailableSeats = findTicket.AvailableSeats - model.AcquiredSeats;
                                updateTIcket.DateCreated = findTicket.DateCreated;
                                updateTIcket.StreamingDateTime = findTicket.StreamingDateTime;

                                var result = _ticket.CreateAsync(updateTIcket);
                                if (result > 0)
                                {
                                    return query.Id;
                                }
                            }
                            else
                            {
                                UpdateTicketDto updateTIcket = new UpdateTicketDto();

                                updateTIcket.Id = model.TicketId;
                                updateTIcket.Description = findTicket.Description;
                                updateTIcket.TotalSeats = findTicket.TotalSeats;
                                updateTIcket.AvailableSeats = findTicket.AvailableSeats - model.AcquiredSeats;
                                updateTIcket.DateCreated = findTicket.DateCreated;
                                updateTIcket.StreamingDateTime = findTicket.StreamingDateTime;

                                var result = _ticket.UpdateAsync(updateTIcket);
                                if (result > 0)
                                {
                                    return query.Id;
                                }
                            }


                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
                else
                {
                    return -1;
                }

                return 0;
            }
            else
            {
                var modelUpdate = _db.tblTransaction.Find(model.Id);
                var findTransaction = GetTransactionsById(model.Id);
                if (modelUpdate != null)
                {
                    Transact insertModel = new Transact();

                    insertModel.Id = model.Id;
                    insertModel.TicketId = model.TicketId > 0 ? model.TicketId : modelUpdate.TicketId;
                    insertModel.AcquiredSeats = model.AcquiredSeats > 0 ? model.AcquiredSeats : modelUpdate.AcquiredSeats;
                    insertModel.StreamingDateTime = model.StreamingDateTime != null ? model.StreamingDateTime : modelUpdate.StreamingDateTime;
                    insertModel.SeatId = model.SeatId > 0 ? model.SeatId : modelUpdate.SeatId;
                    insertModel.PriceId = model.PriceId > 0 ? model.PriceId : modelUpdate.PriceId;

                    var checkAvailability = _seat.CheckAvailableSeat(insertModel.SeatId, insertModel.AcquiredSeats, insertModel.Id, true);
                    if (checkAvailability > 0)
                    {
                        _db.Entry(modelUpdate).State = EntityState.Detached;
                        _db.tblTransaction.Update(insertModel);
                        _db.SaveChanges();

                        //Find Ticket  
                        var findTicket = _ticket.GetAsyncId(insertModel.TicketId);
                        var acquiredSeatVal = 0;
                        if (findTicket != null)
                        {
                            if ((int)(findTicket.AvailableSeats - (findTransaction.AcquiredSeats - insertModel.AcquiredSeats)) >= 0)
                            {
                                acquiredSeatVal = (int)(findTicket.AvailableSeats + (findTransaction.AcquiredSeats - insertModel.AcquiredSeats));
                            }
                            else
                            {
                                acquiredSeatVal = 0;
                            }



                            //Create/Update Ticket
                            if (insertModel.Id <= 0)
                            {
                                CreateTicketDto updateTIcket = new CreateTicketDto();

                                updateTIcket.Id = insertModel.TicketId;
                                updateTIcket.Description = findTicket.Description;
                                updateTIcket.TotalSeats = findTicket.TotalSeats;
                                updateTIcket.AvailableSeats = acquiredSeatVal;
                                updateTIcket.DateCreated = findTicket.DateCreated;
                                updateTIcket.StreamingDateTime = findTicket.StreamingDateTime;

                                var result = _ticket.CreateAsync(updateTIcket);
                                if (result > 0)
                                {
                                    return insertModel.Id;
                                }
                            }
                            else
                            {
                                UpdateTicketDto updateTIcket = new UpdateTicketDto();

                                updateTIcket.Id = insertModel.TicketId;
                                updateTIcket.Description = findTicket.Description;
                                updateTIcket.TotalSeats = findTicket.TotalSeats;
                                updateTIcket.AvailableSeats = acquiredSeatVal;
                                updateTIcket.DateCreated = findTicket.DateCreated;
                                updateTIcket.StreamingDateTime = findTicket.StreamingDateTime;

                                var result = _ticket.UpdateAsync(updateTIcket);
                                if (result > 0)
                                {
                                    return insertModel.Id;
                                }
                            }
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            return 0;

        }
    }
}
