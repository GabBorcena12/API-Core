using API_Core.DBContext;
using API_Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Core.Interface
{
    public class Destination : iDestination
    {
        private TicketDbContext _db;
        public Destination(TicketDbContext db)
        {
            _db = db;
        }
        public int DeleteDestinations(TouristDestination model)
        {
            _db.tblTouristDestinations.Remove(model);
            _db.SaveChanges();
            return 1;
        }

        public List<TouristDestination> GetDestinations()
        {
            var query = _db.tblTouristDestinations.ToList();

            return query;
        }

        public TouristDestination GetDestinationsById(int id)
        {
            var query = _db.tblTouristDestinations.Find(id);

            return query;
        }

        public int ModifyDestinations(TouristDestination model)
        {
            if (model.Id > 0)
            {
                //update
                var modelUpdate = _db.tblTouristDestinations.Find(model.Id);
                if (modelUpdate != null)
                {
                    TouristDestination insertModel = new TouristDestination();

                    insertModel.Id = model.Id;
                    insertModel.Label = modelUpdate.Label; 
                    insertModel.Description = modelUpdate.Description;  
                    insertModel.ImagePath = modelUpdate.ImagePath;  
                    insertModel.URLPath = modelUpdate.URLPath;  

                    _db.Entry<TouristDestination>(modelUpdate).State = EntityState.Detached;
                    _db.tblTouristDestinations.Update(insertModel);
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
                TouristDestination insertModel = new TouristDestination();
                insertModel.Id = model.Id;
                insertModel.Label = model.Label;
                insertModel.Description = model.Description;
                insertModel.ImagePath = model.ImagePath;
                insertModel.URLPath = model.URLPath;

                _db.tblTouristDestinations.Add(insertModel);
                _db.SaveChanges();

                var query = _db.tblTouristDestinations.Where(x => x.Id == model.Id && x.Label == model.Label &&
                x.Description == model.Description).FirstOrDefault();
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
