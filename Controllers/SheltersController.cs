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
    public class SheltersController : ControllerBase
    {
        private readonly IShelterRepository shelterRepo;
        private readonly IMapper mapper;

        public SheltersController(IShelterRepository shelterRepo, IMapper mapper)
        {
            this.shelterRepo = shelterRepo;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ShelterDto>))]
        public IActionResult GetShelters()
        {
            var shelters = shelterRepo.GetShelters();
            var dto = new List<ShelterDto>();

            foreach (var shelter in shelters)
            {
                dto.Add(mapper.Map<ShelterDto>(shelter));
            }
            return Ok(dto);
        }
        [HttpGet("[action]/{shelterId:int}", Name = "GetShelter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShelterDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetShelter(int shelterId)
        {
            var shelter = shelterRepo.GetShelter(shelterId);
            if (shelter == null)
                return NotFound();
            else
                return Ok(mapper.Map<ShelterDto>(shelter));
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ShelterDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateShelter([FromBody] ShelterDto shelterToAdd)
        {
            if (shelterToAdd == null)
                return BadRequest(ModelState);

            if (shelterRepo.ShelterExists(shelterToAdd.Name))
            {
                ModelState.AddModelError("", "Shelter already exist");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shelter = mapper.Map<Shelter>(shelterToAdd);
            if (!shelterRepo.CreateShelter(shelter))
            {
                ModelState.AddModelError("", $"Error with {shelter.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetShelter", new { version=HttpContext.GetRequestedApiVersion().ToString(), shelterId = shelter.Id}, shelter);
        }
        [HttpPatch("[action]/{shelterId:int}", Name = "UpdateShelter")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateShelter(int shelterId, [FromBody] ShelterDto shelterToUpdate)
        {
            if (shelterToUpdate == null || shelterId != shelterToUpdate.Id)
                return BadRequest(ModelState);

            if (shelterRepo.ShelterExists(shelterToUpdate.Name))
            {
                ModelState.AddModelError("", "Shelter already exist");
                return StatusCode(404, ModelState);
            }

            var shelter = mapper.Map<Shelter>(shelterToUpdate);

            if (!shelterRepo.UpdateShelter(shelter))
            {
                ModelState.AddModelError("", $"Error with {shelter.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("[action]/{shelterId:int}", Name = "DeleteShelter")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteShelter(int shelterId)
        {
            if (!shelterRepo.ShelterExists(shelterId))
                return NotFound();

            var shelter = shelterRepo.GetShelter(shelterId);
            if (!shelterRepo.DeleteShelter(shelter))
            {
                ModelState.AddModelError("", $"Error while deleting object {shelter.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
