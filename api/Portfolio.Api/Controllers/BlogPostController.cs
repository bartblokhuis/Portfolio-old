﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces;
using Portfolio.Domain.Dtos.BlogPosts;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
        #region Fields

        private readonly IBlogPostService _blogPostService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPictureService _pictureService;

        #endregion

        #region Constructor

        public BlogPostController(IBlogPostService blogPostService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPictureService pictureService)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(bool includeUnPublished = false)
        {
            if (includeUnPublished && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Ok(await Result.FailAsync("Unauthorized"));

            ListResult<BlogPost> posts = null;

            if(includeUnPublished)
                posts = (await _blogPostService.GetAllBlogPostsAsync()).ToListResult();
            else
                posts = (await _blogPostService.GetPublishedBlogPostsAsync()).ToListResult();

            var result = _mapper.Map<ListResult<ListBlogPostDto>>(posts);
            result.Succeeded = true;
            return Ok(result);
        }

        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, bool includeUnPublished = false)
        {
            if (includeUnPublished && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Ok(await Result.FailAsync("Unauthorized"));

            var post = (await _blogPostService.GetById(id, includeUnPublished));

            if (post == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(post));
            return Ok(result);
        }

        [HttpGet("GetByTitle")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTitle(string title, bool includeUnPublished = false)
        {
            if (includeUnPublished && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Ok(await Result.FailAsync("Unauthorized"));

            var post = (await _blogPostService.GetByTitle(title, includeUnPublished));

            if (post == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(post));
            return Ok(result);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            if (await _blogPostService.IsExistingTitle(dto.Title))
                return Ok(await Result.FailAsync("There is already a blog post with the same title"));

            var blogPost = _mapper.Map<BlogPost>(dto);
            await _blogPostService.Create(blogPost);

            var result = await Result<ListBlogPostDto>.SuccessAsync(_mapper.Map<ListBlogPostDto>(blogPost));
            return Ok(result);
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetById(dto.Id, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.Id} found"));

            if (await _blogPostService.IsExistingTitle(dto.Title, dto.Id))
                return Ok(await Result.FailAsync("There is already a blog post with the same title"));

            _mapper.Map(dto, blogPost);
            await _blogPostService.Update(blogPost);

            var result = await Result<ListBlogPostDto>.SuccessAsync(_mapper.Map<ListBlogPostDto>(blogPost));
            return Ok(result);
        }

        [HttpPut("UpdateBannerPicture")]
        public async Task<IActionResult> UpdateBannerPicture(UpdateBlogPostPictureDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetById(dto.BlogPostId, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.BlogPostId} found"));

            var picture = await _pictureService.GetById(dto.PictureId);
            if (picture == null)
                return Ok(await Result.FailAsync($"No picture with id: {dto.PictureId} found"));

            blogPost.BannerPicture = picture;
            blogPost.BannerPictureId = dto.PictureId;

            await _blogPostService.Update(blogPost);

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(blogPost));
            return Ok(result);
        }

        [HttpPut("UpdateThumbnailPicture")]
        public async Task<IActionResult> UpdateThumbnailPicture(UpdateBlogPostPictureDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetById(dto.BlogPostId, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.BlogPostId} found"));

            var picture = await _pictureService.GetById(dto.PictureId);
            if (picture == null)
                return Ok(await Result.FailAsync($"No picture with id: {dto.PictureId} found"));

            blogPost.Thumbnail = picture;
            blogPost.ThumbnailId = dto.PictureId;

            await _blogPostService.Update(blogPost);

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(blogPost));
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPost = await _blogPostService.GetById(id, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            await _blogPostService.Delete(blogPost);

            var result = await Result.SuccessAsync("Removed the blog post");
            return Ok(result);
        }

        #endregion

        #endregion
    }
}
