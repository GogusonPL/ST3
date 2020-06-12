using ST3.Data;
using ST3.Models;
using ST3.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDbContext database;

        public ShelterRepository(ApplicationDbContext context)
        {
            database = context;
        }
        public bool CreateShelter(Shelter nationalPark)
        {
            database.Shelters.Add(nationalPark);
            return Save();
        }

        public bool DeleteShelter(Shelter nationalPark)
        {
            database.Shelters.Remove(nationalPark);
            return Save();
        }

        public Shelter GetShelter(int id)
        {
            return database.Shelters.SingleOrDefault(entity => entity.Id == id);
        }

        public ICollection<Shelter> GetShelters()
        {
            return database.Shelters.OrderBy(entity => entity.Name).ToList();
        }

        public bool ShelterExists(string name)
        {
            return database.Shelters.Any(entity => entity.Name == name);
        }

        public bool ShelterExists(int id)
        {
            return database.Shelters.Any(entity => entity.Id == id);
        }

        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateShelter(Shelter nationalPark)
        {
            database.Shelters.Update(nationalPark);
            return Save();
        }
    }
}
