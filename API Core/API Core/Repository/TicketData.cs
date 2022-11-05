using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Core.Repository
{
    public class TicketData : iTicket
    {
        private TicketDbContext _db;
        private readonly IMapper _mapper;
        public TicketData(TicketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public List<GetTicketDto> GetAsync()
        {
            var result = _db.tblTicket.ToList();
            var ticket = _mapper.Map<List<GetTicketDto>>(result);
            return ticket;
        }
        public GetTicketDto GetAsyncId(int id)
        {
            var result = _db.tblTicket.Find(id);
            var ticket = _mapper.Map<GetTicketDto>(result);
            return ticket;
        }

        public int DeleteAsyncId(int id)
        {
            var findById = _db.tblTicket.Find(id);
            if (findById == null)
            {
                return 0;
            }

            Ticket ticket = _mapper.Map<Ticket>(findById);
            try
            {
                _db.tblTicket.Remove(ticket);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public int CreateAsync(CreateTicketDto model)
        {
            if (model.Id <= 0)
            {
                var ticket = _mapper.Map<Ticket>(model);


                try
                {
                    _db.tblTicket.Add(ticket);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //Check if it is inserted correctly
                var query = _db.tblTicket.Where(t => t.StreamingDateTime == ticket.StreamingDateTime && t.Description == ticket.Description).FirstOrDefault();

                if (query != null)
                {
                    return query.Id;
                }
                return 0;
            }

            return 0;

        }

        public int UpdateAsync(UpdateTicketDto modelUpdateDto)
        {
            var modelUpdate = _db.tblTicket.Find(modelUpdateDto.Id);
            if (modelUpdate == null)
            {
                return 0;
            }


            Ticket insertModel = _mapper.Map<Ticket>(modelUpdateDto);
            try
            {
                _db.Entry(modelUpdate).State = EntityState.Detached;
                _db.tblTicket.Update(insertModel);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            return modelUpdateDto.Id;
        }
    }
}
