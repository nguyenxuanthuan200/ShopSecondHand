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
    [Route("api/BuildingController")]
    public class BuildingController : Controller
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
        [HttpGet("{name}")]
        public async Task<ActionResult<GetBuildingResponse>> GetBuildingByName(String name)
        {
            try
            {
                var result = await buildingRepository.GetBuildingByName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpPost]
        public async Task<ActionResult<CreateBuildingResponse>> CreateBuilding([FromBody] CreateBuildingRequest buildingRequest)
        {
            try
            {
                if (buildingRequest == null)
                {
                    return BadRequest();
                }
                var createBuilding = await buildingRepository.AddBuilding(buildingRequest);
                if (createBuilding != null)
                {
                    return Ok(createBuilding);

                    //return CreatedAtAction(nameof(GetAccountById),
                    // new { id = createdAccount.AccountId }, createdAccount);
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Name da ton tai.");



            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<UpdateBuildingResponse>> UpdateBuilding(Guid id, [FromBody] UpdateBuildingRequest buildingRequest)
        {
            try
            {
                var updateBuilding = await buildingRepository.UpdateBuilding(id, buildingRequest);

                if (updateBuilding == null)
                {
                    return NotFound($"Building  with Id = {id} not found!");
                }

                return Ok($"update thanh cong");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Store!");
            }

        }
        [HttpDelete("{id:Guid}")]
        public void DeleteBuilding(Guid id)
        {
            try
            {
                var updateBuilding = buildingRepository.GetBuildingById(id);

                if (updateBuilding == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                buildingRepository.DeleteBuilding(id);
                Ok();
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Account!");
            }
        }


    }
}
