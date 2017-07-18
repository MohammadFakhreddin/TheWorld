using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.models;
using Microsoft.Extensions.Logging;
using TheWorld.viewModels;
using AutoMapper;
using TheWorld.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.controllers.api
{
    [Authorize]
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;
        private GeoCoordsService _coordService;
        public StopsController(IWorldRepository _repository, 
            ILogger<StopsController> _logger,
            GeoCoordsService _coordService)
        {
            this._logger = _logger;
            this._repository = _repository;
            this._coordService = _coordService;
        }
        public IActionResult Get(String tripName)
        {
            try
            {
                Trip trip = _repository.getTripByName(tripName,User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.stops.OrderBy(s=>s.order)));
            }catch(Exception e)
            {
                _logger.LogError($"Failed to get stops for {tripName} Exception is:\n{e}");
            }
            return BadRequest("Failed to get stops");
        }
        [HttpPost("")]
        public async Task<IActionResult> Post(String tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Stop newStop = Mapper.Map<Stop>(vm);

                    GeoCoordsResult geoCoordsResult = await _coordService.getCoordsAsync(newStop.name);
                    if (!geoCoordsResult.success)
                    {
                        _logger.LogError(geoCoordsResult.message);
                    }
                    else
                    {
                        newStop.latitude = geoCoordsResult.latitude;
                        newStop.longtitude = geoCoordsResult.longtitude;

                        _repository.addStop(tripName, newStop,User.Identity.Name);

                        if (await _repository.saveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.name}",
                                Mapper.Map<StopViewModel>(newStop));
                        }
                        else
                        {
                            return BadRequest("Save changes failed");
                        }
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save stop for {vm.name} Exception is:\n{e}");
            }
            return BadRequest("Failed to save new stop");
        }
    }
}
