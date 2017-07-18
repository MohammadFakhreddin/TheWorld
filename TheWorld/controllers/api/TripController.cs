using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.models;
using TheWorld.viewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.controllers.api
{
    [Route("api/trips")]
    [Authorize]
    public class TripController: Controller
    {
        private IWorldRepository _repository;
        private ILogger _logger;
        public TripController(IWorldRepository _repository,ILogger<TripController>_logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try {
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(_repository.getAllTripsWithStops(User.Identity.Name)));
            }catch(Exception e)
            {
                _logger.LogError($"Failed to get all trips:{e}");
                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel tripVModel)
        {
            if (ModelState.IsValid)
            {
                Trip trip = Mapper.Map<Trip>(tripVModel);
                trip.username = User.Identity.Name;
                _repository.addTrip(trip);
                if (await _repository.saveChangesAsync())
                {
                    return Created($"api/trips/{trip.name}", trip);
                }
                else
                {
                    return BadRequest("Internal server error");
                }
            }
            //return BadRequest("Something went wrong in creating new trip");
            return BadRequest(ModelState);//Just for test
        }
    }
}
