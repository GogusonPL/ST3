using ST3.Models;
using System.Collections.Generic;

namespace ST3.Repositories.Interfaces
{
    public interface IShelterRepository
    {
        ICollection<Shelter> GetShelters();
        Shelter GetShelter(int id);
        bool ShelterExists(string name);
        bool ShelterExists(int id);
        bool CreateShelter(Shelter nationalPark);
        bool UpdateShelter(Shelter nationalPark);
        bool DeleteShelter(Shelter nationalPark);
        bool Save();
    }
}
