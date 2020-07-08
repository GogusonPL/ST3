using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ST3.Dtos;
using ST3.Models;
using ST3.Repositories.Interfaces;

namespace ST3.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/request")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository animalRepo;
        private readonly IMapper mapper;

        public AnimalController(IAnimalRepository animalRepo, IMapper mapper)
        {
            this.animalRepo = animalRepo;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AnimalDto>))]
        public IActionResult GetAnimals()
        {
            var animals = animalRepo.GetAnimals();
            var dto = new List<AnimalDto>();

            foreach (var animal in animals)
            {
                dto.Add(mapper.Map<AnimalDto>(animal));
            }
            return Ok(dto);
        }
        [HttpGet("[action]/{shelterId:int}", Name = "GetAnimalsInShelter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AnimalDto>))]
        public IActionResult GetAnimalsInShelter(int shelterId)
        {
            var animals = animalRepo.GetAnimalsInShelter(shelterId);
            var dto = new List<AnimalDto>();

            foreach (var animal in animals)
            {
                dto.Add(mapper.Map<AnimalDto>(animal));
            }
            return Ok(dto);
        }
        [HttpGet("[action]/{animalId:int}", Name = "GetAnimal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnimalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAnimal(int animalId)
        {
            var animal = animalRepo.GetAnimal(animalId);
            if (animal == null)
                return NotFound();
            else
                return Ok(mapper.Map<AnimalDto>(animal));
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AnimalCreateDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAnimal([FromBody] AnimalCreateDto animalToAdd)
        {
            if (animalToAdd == null)
                return BadRequest(ModelState);

            if (animalRepo.AnimalExists(animalToAdd.Name))
            {
                ModelState.AddModelError("", "Animal already exist");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var animal = mapper.Map<Animal>(animalToAdd);
            if (!animalRepo.CreateAnimal(animal))
            {
                ModelState.AddModelError("", $"Error with {animal.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetAnimal", new { version = HttpContext.GetRequestedApiVersion().ToString(), animalId = animal.Id }, animal);
        }
        [HttpPatch("[action]/{animalId:int}", Name = "UpdateAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnimal(int animalId, [FromBody] AnimalUpdateDto animalToUpdate)
        {
            if (animalToUpdate == null || animalId != animalToUpdate.Id)
                return BadRequest(ModelState);

            if (animalRepo.AnimalExists(animalToUpdate.Name))
            {
                ModelState.AddModelError("", "Animal already exist");
                return StatusCode(404, ModelState);
            }

            var animal = mapper.Map<Animal>(animalToUpdate);

            if (!animalRepo.UpdateAnimal(animal))
            {
                ModelState.AddModelError("", $"Error with {animal.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("[action]/{animalId:int}", Name = "DeleteAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAnimal(int animalId)
        {
            if (!animalRepo.AnimalExists(animalId))
                return NotFound();

            var animal = animalRepo.GetAnimal(animalId);
            if (!animalRepo.DeleteAnimal(animal))
            {
                ModelState.AddModelError("", $"Error while deleting object {animal.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
