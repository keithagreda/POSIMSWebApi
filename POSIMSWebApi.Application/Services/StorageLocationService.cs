using Domain.Entities;
using Domain.Error;
using Domain.Interfaces;
using POSIMSWebApi.Application.Dtos.StorageLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class StorageLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StorageLocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<Result> ValidateStorageLocation(CreateOrEditStorageLocationDto input)
        {
            var isExisting = new StorageLocation();
            if(input.Id is not null)
            {
                isExisting = await _unitOfWork.StorageLocation.FirstOrDefaultAsync(e => e.Name == input.Name && e.Id != input.Id);
                if (isExisting != null)
                {
                    return Result.Failure(StorageLocationErrors.SameLocationName);
                }

                return Result.Success();
            }
            isExisting = await _unitOfWork.StorageLocation.FirstOrDefaultAsync(e => e.Name == input.Name);
            if (isExisting != null)
            {
                return Result.Failure(StorageLocationErrors.SameLocationName);
            }

            return Result.Success();
        }
        public async Task<Result> CreateStorageLocation(CreateOrEditStorageLocationDto input)
        {
            var validation = await ValidateStorageLocation(input);
            if(validation != Result.Success())
            {
                return validation;
            }
            var newStorageLoc = new StorageLocation
            {
                Name = input.Name,
                Description = input.Description,
            };

            await _unitOfWork.StorageLocation.AddAsync(newStorageLoc);
            _unitOfWork.Complete();
            return Result.Success();
        }

        
    }
}
