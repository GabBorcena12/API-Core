using API_Core.Model.Models;
using System.Collections.Generic;

namespace API_Core.Interface
{
    public interface IDestination
    {
        public List<TouristDestination> GetDestinations();
        public TouristDestination GetDestinationsById(int id);
        public int ModifyDestinations(TouristDestination model);
        public int DeleteDestinations(TouristDestination model);
    }
}
