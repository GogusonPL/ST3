﻿using ST3.Models;
using System.Collections.Generic;

namespace ST3.Repositories.Interfaces
{
    public interface IShelterRepository
    {
        ICollection<Shelter> GetShelters();
        Shelter GetShelter(int id);
        bool ShelterExists(string name);
        bool ShelterExists(int id);
        bool CreateShelter(Shelter shelter);
        bool UpdateShelter(Shelter shelter);
        bool DeleteShelter(Shelter shelter);
        bool Save();
    }
}
