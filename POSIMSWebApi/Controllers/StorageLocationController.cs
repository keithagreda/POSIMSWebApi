﻿using Domain.Error;
using Domain.Interfaces;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSIMSWebApi.Application.Dtos.StorageLocation;
using POSIMSWebApi.Application.Interfaces;

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
        [HttpGet("GetAllStorageLocation")]
        public async Task<IActionResult> GetAllStorageLocation()
        {
            var result = await _unitOfWork.StorageLocation.GetAllAsync();
            return Ok(result);
        }
    }
}