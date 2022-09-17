﻿using API_Core.Model;
using System.Collections.Generic;

namespace API_Core.Interface
{
    public interface iDestination
    {
        public List<TouristDestination> GetDestinations();
        public TouristDestination GetDestinationsById(int id);
        public int ModifyDestinations(TouristDestination model);
        public int DeleteDestinations(TouristDestination model);
    }
}