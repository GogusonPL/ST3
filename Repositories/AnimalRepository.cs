using Microsoft.EntityFrameworkCore;
using ST3.Data;
using ST3.Models;
using ST3.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext database;

        public AnimalRepository(ApplicationDbContext context)
        {
            database = context;
        }
        public bool CreateAnimal(Animal animal)
        {
            database.Animals.Add(animal);
            return Save();
        }

        public bool DeleteAnimal(Animal animal)
        {
            database.Animals.Remove(animal);
            return Save();
        }

        public Animal GetAnimal(int id)
        {
            return database.Animals.Include(x => x.Shelter).SingleOrDefault(entity => entity.Id == id);
        }

        public ICollection<Animal> GetAnimals()
        {
            return database.Animals.Include(x => x.Shelter).OrderBy(entity => entity.Name).ToList();
        }

        public bool AnimalExists(string name)
        {
            return database.Animals.Any(entity => entity.Name == name);
        }

        public bool AnimalExists(int id)
        {
            return database.Animals.Any(entity => entity.Id == id);
        }

        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAnimal(Animal animal)
        {
            database.Animals.Update(animal);
            return Save();
        }

        public ICollection<Animal> GetAnimalsInShelter(int shelterId)
        {
            return database.Animals.Include(x => x.Shelter).Where(x => x.Shelter.Id == shelterId).ToList();
        }
    }
}
