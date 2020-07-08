using ST3.Models;
using System.Collections.Generic;

namespace ST3.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        ICollection<Animal> GetAnimals();
        Animal GetAnimal(int id);
        bool AnimalExists(string name);
        bool AnimalExists(int id);
        bool CreateAnimal(Animal animal);
        bool UpdateAnimal(Animal animal);
        bool DeleteAnimal(Animal animal);
        ICollection<Animal> GetAnimalsInShelter(int shelterId);
        bool Save();
    }
}
