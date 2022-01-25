using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Helpers;
using Portfolio.Domain.Dtos.BlogPosts;
using Portfolio.Domain.Dtos.Comments;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Blogs;
using Portfolio.Services.Comments;
using Portfolio.Services.Pictures;
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
        private readonly IBlogPostCommentService _blogPostCommentService;

        #endregion

        #region Constructor

        public BlogPostController(IBlogPostService blogPostService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPictureService pictureService, IBlogPostCommentService blogPostCommentService)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _pictureService = pictureService;
            _blogPostCommentService = blogPostCommentService;
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

            var post = (await _blogPostService.GetByIdAsync(id, includeUnPublished));

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

            var post = (await _blogPostService.GetByTitleAsync(title, includeUnPublished));

            if (post == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(post));
            return Ok(result);
        }

        [HttpGet("Comments/GetByBlogPostId")]
        public async Task<IActionResult> GetCommentsByBlogPostId(int blogPostId)
        {
            var post = (await _blogPostService.GetByIdAsync(blogPostId, true));

            if (post == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            if(post.Comments == null)
                post.Comments = new List<Comment>();

            var comments = post.Comments.ToListResult();
            var result = _mapper.Map<ListResult<ListCommentDto>>(comments);
            result.Succeeded = true;
            return Ok(result);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            if (await _blogPostService.IsExistingTitleAsync(dto.Title))
                return Ok(await Result.FailAsync("There is already a blog post with the same title"));

            var blogPost = _mapper.Map<BlogPost>(dto);
            await _blogPostService.InsertAsync(blogPost);

            var result = await Result<ListBlogPostDto>.SuccessAsync(_mapper.Map<ListBlogPostDto>(blogPost));
            return Ok(result);
        }

        [HttpPost("Comment")]
        [AllowAnonymous]
        public async Task<IActionResult> Comment(CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            //If the client is authenticated include all blog posts.
            var includeUnPublished = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            //Email address is optional however, if the client provides an email ensure that it's a valid email.
            if(!string.IsNullOrEmpty(dto.Email) && !CommonHelper.IsValidEmail(dto.Email))
                return Ok(await Result.FailAsync($"Please use a real email address"));

            if(string.IsNullOrEmpty(dto.Content))
                return Ok(await Result.FailAsync($"Please enter your comment"));

            if(dto.Content.Length > 512)
                return Ok(await Result.FailAsync($"Please don't use more than 512 charachters in your comment"));

            //Ensures that the comment doesn't create an unnecesary relation to the blog post.
            if (dto.BlogPostId != null && dto.ParentCommentId != null)
                dto.BlogPostId = null;

            //Ensure that the blog post exists.
            if(dto.BlogPostId != null)
            {
                var blogPost = await _blogPostService.GetByIdAsync((int)dto.BlogPostId, includeUnPublished);
                if (blogPost == null)
                    return Ok(await Result.FailAsync($"No blog post with id: {dto.BlogPostId} found"));
            }
            
            //If the comment is a reply to a previous comment ensure that the first comment exists.
            if(dto.ParentCommentId != null)
            {
                var parentComment = await _blogPostCommentService.GetByIdAsync((int)dto.ParentCommentId);
                if (parentComment == null)
                    return Ok(await Result.FailAsync($"No comment with id: {dto.ParentCommentId} found"));
            }

            var comment = _mapper.Map<Comment>(dto);

            //Currently there is only one user (admin), therefore if client is authenticated it must be the author.
            comment.IsAuthor = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            await _blogPostCommentService.InsertAsync(comment);
            var result = await Result<CommentDto>.SuccessAsync(_mapper.Map<CommentDto>(comment));
            return Ok(result);
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetByIdAsync(dto.Id, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.Id} found"));

            if (await _blogPostService.IsExistingTitleAsync(dto.Title, dto.Id))
                return Ok(await Result.FailAsync("There is already a blog post with the same title"));

            _mapper.Map(dto, blogPost);
            await _blogPostService.UpdateAsync(blogPost);

            var result = await Result<ListBlogPostDto>.SuccessAsync(_mapper.Map<ListBlogPostDto>(blogPost));
            return Ok(result);
        }

        [HttpPut("UpdateBannerPicture")]
        public async Task<IActionResult> UpdateBannerPicture(UpdateBlogPostPictureDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetByIdAsync(dto.BlogPostId, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.BlogPostId} found"));

            var picture = await _pictureService.GetByIdAsync(dto.PictureId);
            if (picture == null)
                return Ok(await Result.FailAsync($"No picture with id: {dto.PictureId} found"));

            blogPost.BannerPicture = picture;
            blogPost.BannerPictureId = dto.PictureId;

            await _blogPostService.UpdateAsync(blogPost);

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(blogPost));
            return Ok(result);
        }

        [HttpPut("UpdateThumbnailPicture")]
        public async Task<IActionResult> UpdateThumbnailPicture(UpdateBlogPostPictureDto dto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            var blogPost = await _blogPostService.GetByIdAsync(dto.BlogPostId, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync($"No blog post with id: {dto.BlogPostId} found"));

            var picture = await _pictureService.GetByIdAsync(dto.PictureId);
            if (picture == null)
                return Ok(await Result.FailAsync($"No picture with id: {dto.PictureId} found"));

            blogPost.Thumbnail = picture;
            blogPost.ThumbnailId = dto.PictureId;

            await _blogPostService.UpdateAsync(blogPost);

            var result = await Result<BlogPostDto>.SuccessAsync(_mapper.Map<BlogPostDto>(blogPost));
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPost = await _blogPostService.GetByIdAsync(id, true);
            if (blogPost == null)
                return Ok(await Result.FailAsync("Blog post not found"));

            await _blogPostService.DeleteAsync(blogPost);

            var result = await Result.SuccessAsync("Removed the blog post");
            return Ok(result);
        }

        [HttpDelete("Comment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _blogPostCommentService.GetByIdAsync(id);
            if(comment == null)
                return Ok(await Result.FailAsync("Comment not found"));

            await _blogPostCommentService.DeleteAsync(id);
            var result = await Result.SuccessAsync("Removed the comment");
            return Ok(result);
        }

        #endregion

        #endregion
    }
}
