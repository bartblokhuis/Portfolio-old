﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Services.Pictures;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PictureController : ControllerBase
    {
        #region Fields

        private readonly IPictureService _pictureService;

        #endregion

        #region Constructor

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pictures = await _pictureService.GetAllAsync();
            var result = await Result<IEnumerable<Picture>>.SuccessAsync(pictures);
            return Ok(result);
        }

        #endregion

        #region Post

        [HttpPost()]
        public async Task<IActionResult> UploadPicture(IFormFile file, string titleAttribute, string altAttribute)
        {
            if(file == null)
                return Ok(await Result.FailAsync($"Please provide the picture file"));
            
            var picture = await _pictureService.CreatePictureFromFileAsync(file, titleAttribute, altAttribute);
            var result = await Result<Picture>.SuccessAsync(picture);
            return Ok(result);
        }

        #endregion

        #region Put

        [HttpPut()]
        public async Task<IActionResult> UpdatePicture(string titleAttribute, string altAttribute, int pictureId, IFormFile file = null)
        {
            var picture = await _pictureService.GetByIdAsync(pictureId);

            if(picture == null)
                return Ok(await Result.FailAsync($"There is no picture with the id: ${pictureId}"));

            await _pictureService.UpdatePictureFromFileAsync(picture, file, titleAttribute, altAttribute);
            var result = await Result<Picture>.SuccessAsync(picture);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete()]
        public async Task<IActionResult> Delete(int pictureId)
        {
            var picture = await _pictureService.GetByIdAsync(pictureId);

            if (picture == null)
                return Ok(await Result.FailAsync($"There is no picture with the id: ${pictureId}"));

            var error = await _pictureService.DeleteAsync(picture);

            return string.IsNullOrEmpty(error) ?
                Ok(await Result.SuccessAsync("Removed the picture")) : 
                Ok(await Result.FailAsync(error));
        }

        #endregion

        #endregion
    }
}
