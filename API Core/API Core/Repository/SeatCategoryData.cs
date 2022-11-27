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
    public class SeatCategoryData : iSeatCategory
    {
        private TicketDbContext _db;
        private readonly IMapper _mapper;
        public SeatCategoryData(TicketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public List<SeatCategoryDto> GetAsync()
        {
            var result = _db.tblSeatCategory.ToList();
            var result2 = _mapper.Map<List<SeatCategoryDto>>(result);
            return result2;
        }
        public SeatCategoryDto GetAsyncId(int id)
        {
            var result = _db.tblSeatCategory.Find(id);
            var result2 = _mapper.Map<SeatCategoryDto>(result);
            return result2;
        }

        public int DeleteAsyncId(int id)
        {
            var findById = _db.tblSeatCategory.Find(id);
            if (findById == null)
            {
                return 0;
            }

            SeatCategory seat = _mapper.Map<SeatCategory>(findById);
            try
            {
                _db.tblSeatCategory.Remove(seat);
                _db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }


        public int CreateAsync(SeatCategoryDto model)
        {
            if (model.Id <= 0)
            {
                var result = _mapper.Map<SeatCategory>(model);


                try
                {
                    _db.tblSeatCategory.Add(result);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //Check if it is inserted correctly
                var query = _db.tblSeatCategory.Where(t => t.Code == result.Code && t.Description == result.Description).FirstOrDefault();

                if (query != null)
                {
                    return query.Id;
                }
                return 0;
            }

            return 0;

        }

        public int UpdateAsync(SeatCategoryDto modelUpdateDto)
        {
            var modelUpdate = _db.tblSeatCategory.Find(modelUpdateDto.Id);
            if (modelUpdate == null)
            {
                return 0;
            }


            SeatCategory insertModel = _mapper.Map<SeatCategory>(modelUpdateDto);
            try
            {
                _db.Entry(modelUpdate).State = EntityState.Detached;
                _db.tblSeatCategory.Update(insertModel);
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
