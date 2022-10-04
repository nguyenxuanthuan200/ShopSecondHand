using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.BuildingRequest;
using ShopSecondHand.Data.ResponseModels.BuildingResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.BuildingRepository;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingRepository buildingRepository;
        public BuildingController(IBuildingRepository buildingRepository)
        {
            this.buildingRepository = buildingRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetBuilding()
        {
            try
            {
                return Ok(await buildingRepository.GetBuilding());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetBuildingByName(string name)
        {
            try
            {
                var result = await buildingRepository.GetBuildingByName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBuildingById(Guid id)
        {
            try
            {
                var result = await buildingRepository.GetBuildingById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateBuilding(CreateBuildingRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                var create = await buildingRepository.CreateBuilding(request);
                if (create != null)
                {
                    return Ok(create);
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Building da ton tai.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBuilding(Guid id, UpdateBuildingRequest request)
        {
            try
            {
                var update = await buildingRepository.UpdateBuilding(id, request);

                if (update == null)
                {
                    return NotFound();
                }

                return Ok(update);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Store!");
            }

        }
        [HttpDelete("{id}")]
        public void DeleteBuilding(Guid id)
        {
            try
            {
                var delete = buildingRepository.GetBuildingById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                buildingRepository.DeleteBuilding(id);
                StatusCode(StatusCodes.Status200OK,
                   "Delete Success.");
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting!");
            }
        }


    }
}
