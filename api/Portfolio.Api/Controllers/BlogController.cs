using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces;
using Portfolio.Domain.Dtos.Blogs;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Portfolio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        #region Fields

        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public BlogController(IBlogService blogService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _blogService = blogService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(bool includeUnPublished = false)
        {
            if (includeUnPublished && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Ok(await Result.FailAsync("Unauthorized"));

            var posts = (await _blogService.Get(includeUnPublished)).ToListResult();

            var result = _mapper.Map<ListResult<ListBlogDto>>(posts);
            result.Succeeded = true;
            return Ok(result);
        }

        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, bool includeUnPublished = false)
        {
            if (includeUnPublished && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Ok(await Result.FailAsync("Unauthorized"));

            var post = (await _blogService.GetById(id, includeUnPublished));

            if(post == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            var result = await Result<BlogDto>.SuccessAsync(_mapper.Map<BlogDto>(post));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            if (await _blogService.IsExistingTitle(dto.Title))
                return Ok(await Result.FailAsync("There is already a blog with the same title"));

            var blog = _mapper.Map<Blog>(dto);
            await _blogService.Create(blog);

            var result = await Result<ListBlogDto>.SuccessAsync(_mapper.Map<ListBlogDto>(blog));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blog = await _blogService.GetById(dto.Id, true);
            if (blog == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.Id} found"));

            if (await _blogService.IsExistingTitle(dto.Title, dto.Id))
                return Ok(await Result.FailAsync("There is already a blog post with the same title"));

            _mapper.Map(dto, blog);
            await _blogService.Update(blog);

            var result = await Result<ListBlogDto>.SuccessAsync(_mapper.Map<ListBlogDto>(blog));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetById(id, true);
            if (blog == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            await _blogService.Delete(blog);

            var result = await Result.SuccessAsync("Removed the blog post");
            return Ok(result);
        }

        #endregion


    }
}
