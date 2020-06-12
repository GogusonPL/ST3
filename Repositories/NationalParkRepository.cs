using ST3.Data;
using ST3.Models;
using ST3.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.Repositories
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext database;

        public NationalParkRepository(ApplicationDbContext context)
        {
            database = context;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            database.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            database.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int id)
        {
            return database.NationalParks.SingleOrDefault(entity => entity.Id == id);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return database.NationalParks.OrderBy(entity => entity.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return database.NationalParks.Any(entity => entity.Name == name);
        }

        public bool NationalParkExists(int id)
        {
            return database.NationalParks.Any(entity => entity.Id == id);
        }

        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            database.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
