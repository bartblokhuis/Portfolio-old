using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Helpers;
using Portfolio.Core.Services.BlogSubscribers;
using Portfolio.Domain.Dtos.BlogSubscribers;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BlogSubscriberController : ControllerBase
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IBlogSubscriberService _blogSubscriberService;

    #endregion

    #region Constructor

    public BlogSubscriberController(IMapper mapper, IBlogSubscriberService blogSubscriberService)
    {
        _mapper = mapper;
        _blogSubscriberService = blogSubscriberService;
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = (await _blogSubscriberService.GetAllAsync()).ToListResult();

        var result = _mapper.Map<ListResult<ListBlogSubscriberDto>>(subscribers);
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpGet("GetById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var subscriber = await _blogSubscriberService.GetByIdAsync(id);

        if (subscriber == null)
            return Ok(await Result.FailAsync("Blog subscriber not found"));

        var result = await Result<BlogSubscriberDto>.SuccessAsync(_mapper.Map<BlogSubscriberDto>(subscriber));
        return Ok(result);
    }

    #endregion

    #region Post

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Subscribe(CreateBlogSubscriberDto dto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Invalid model");

        if(string.IsNullOrEmpty(dto.EmailAddress) || !CommonHelper.IsValidEmail(dto.EmailAddress))
            return Ok(await Result.FailAsync("Please enter a real email address"));

        if (await _blogSubscriberService.Exists(dto.EmailAddress))
            return Ok(await Result.FailAsync("Email is already subscribed"));

        var blogSubscriber = _mapper.Map<BlogSubscriber>(dto);
        await _blogSubscriberService.SubscribeAsync(blogSubscriber);

        var result = await Result<ListBlogSubscriberDto>.SuccessAsync(_mapper.Map<ListBlogSubscriberDto>(blogSubscriber));
        return Ok(result);
    }

    #endregion

    #region Delete

    [HttpDelete]
    [AllowAnonymous]
    public async Task<IActionResult> Unsubscribe(Guid id)
    {
        var blogSubscriber = await _blogSubscriberService.GetByIdAsync(id);
        if (blogSubscriber == null)
            return Ok(await Result.FailAsync("Blog subscriber not found"));

        await _blogSubscriberService.UnsubscribeAsync(blogSubscriber);

        var result = await Result.SuccessAsync("Unsubscribed");
        return Ok(result);
    }

    #endregion

    #endregion
}

