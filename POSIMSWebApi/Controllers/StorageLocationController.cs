using Domain.ApiResponse;
using Domain.Error;
using Domain.Interfaces;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.StorageLocation;
using POSIMSWebApi.Application.Interfaces;
using POSIMSWebApi.Authentication;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageLocationController : ControllerBase
    {
        private readonly IStorageLocationService _storageLocationService;
        private readonly IUnitOfWork _unitOfWork;
        public StorageLocationController(IStorageLocationService storageLocationService, IUnitOfWork unitOfWork)
        {
            _storageLocationService = storageLocationService;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Inventory + "," + UserRole.Cashier)]
        [HttpPost("CreateStorageLocation")]
        public async Task<IActionResult> CreateStorageLocation([FromBody]CreateOrEditStorageLocationDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _storageLocationService.CreateStorageLocation(input);
            return result.Match<IActionResult>(
                success => CreatedAtAction(nameof(CreateStorageLocation), new { id = input.Id }, success),
                error => BadRequest(error));
        }
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Inventory + "," + UserRole.Cashier)]
        [HttpGet("GetAllStorageLocation")]
        public async Task<ActionResult<ApiResponse<List<GetStorageLocationForDropDownDto>>>> GetAllStorageLocation()
        {
            var result = await _unitOfWork.StorageLocation.GetQueryable()
                .OrderBy(e => e.Name)
                .Select(e => new GetStorageLocationForDropDownDto
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToListAsync();

            return Ok(ApiResponse<List<GetStorageLocationForDropDownDto>>.Success(result));
        }
    }
}
