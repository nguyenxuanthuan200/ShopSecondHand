﻿using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.BuildingRequest;
using ShopSecondHand.Data.ResponseModels.BuildingResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.BuildingRepository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ShopSecondHandContext dbContext;

        public BuildingRepository(ShopSecondHandContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CreateBuildingResponse> AddBuilding(CreateBuildingRequest buildingRequest)
        {
            var building = await dbContext.Buildings
                 .FirstOrDefaultAsync(p => p.Name.Equals(buildingRequest.Name));
            if (building != null)
                return null;

            Building buildingg = new Building();
            {
                buildingg.Id = new Guid();
                buildingg.Name = buildingRequest.Name;
                buildingg.Address = buildingRequest.Address;
            };
            var result = await dbContext.Buildings.AddAsync(buildingg);
            await dbContext.SaveChangesAsync();
            var re = new CreateBuildingResponse()
            {
                Name = buildingg.Name,
                Address = buildingg.Address
            };
            return re;

        }

        public  void DeleteBuilding(Guid id)
        {
            var deBuilding = dbContext.Buildings
                .SingleOrDefault(p => p.Id == id);
            if (deBuilding == null)
            {
                throw new Exception("This Building is unavailable!");
            }
               dbContext.Buildings.Remove(deBuilding);
               dbContext.SaveChangesAsync();
              
        }
        public async Task<IEnumerable<GetBuildingResponse>> GetBuilding()

        {
            var building = await dbContext.Buildings.ToListAsync();
            IEnumerable<GetBuildingResponse> result = building.Select(
                x =>
                {
                    return new GetBuildingResponse()
                    {
                        Name = x.Name,
                        Address = x.Address
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetBuildingResponse> GetBuildingByName(string name)
        {
            var getByName = await dbContext.Buildings

                .FirstOrDefaultAsync(p => p.Name.Equals(name));
            if (getByName != null)
            {
                var re = new GetBuildingResponse()
                {
                    Name = getByName.Name,
                    Address = getByName.Address
                };
                return re;
            }
            return null;
        }
        public async Task<GetBuildingResponse> GetBuildingById(Guid id)
        {
            var getById = await dbContext.Buildings

                .FirstOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetBuildingResponse()
                {
                    Name = getById.Name,
                    Address = getById.Address
                };
                return re;
            }
            return null;
        }
        public async Task<UpdateBuildingResponse> UpdateBuilding(Guid id, UpdateBuildingRequest buildingRequest)
        {
            var upBuilding = await dbContext.Buildings.SingleOrDefaultAsync(c => c.Id == id);

            if (upBuilding == null) return null;
            upBuilding.Name = buildingRequest.Name;
            upBuilding.Address = buildingRequest.Address;
            dbContext.Buildings.Update(upBuilding);
            await dbContext.SaveChangesAsync();

            var up = new UpdateBuildingResponse()
            {
                Name = upBuilding.Name,
                Address = upBuilding.Address,

            };
            return up;
        }

       
    }
}
