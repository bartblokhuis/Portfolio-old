using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Helpers;
using Portfolio.Domain.Dtos.BlogSubscribers;
using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Extensions;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.BlogSubscribers;
using System;
using System.Collections.Generic;
using System.Linq;
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

    #region Utils

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

    [HttpGet("LoadBlogSubscriberStatistics")]
    public async Task<IActionResult> LoadBlogSubscriberStatistics(string period)
    {
        var resultData = new List<object>();
        var nowDt = DateTime.Now;

        switch (period)
        {
            case "year":
                var yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
                var searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);

                for (var i = 0; i <= 12; i++)
                {
                    resultData.Add(new
                    {
                        date = searchYearDateUser.Date.ToString("Y"),
                        value = (await _blogSubscriberService.GetAllAsync(searchYearDateUser, searchYearDateUser.AddMonths(1), true)).TotalCount.ToString()
                    });

                    searchYearDateUser = searchYearDateUser.AddMonths(1);
                }
                break;

            case "month":
                var monthAgoDt = nowDt.AddDays(-30);
                var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                for (var i = 0; i <= 30; i++)
                {
                    resultData.Add(new
                    {
                        date = searchMonthDateUser.Date.ToString("M"),
                        value = (await _blogSubscriberService.GetAllAsync(searchMonthDateUser, searchMonthDateUser.AddDays(1), true)).TotalCount.ToString()
                    });

                    searchMonthDateUser = searchMonthDateUser.AddDays(1);
                }

                break;
            case "week":
            default:
                //week statistics
                var weekAgoDt = nowDt.AddDays(-7);
                var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                for (var i = 0; i <= 7; i++)
                {
                    resultData.Add(new
                    {
                        date = searchWeekDateUser.Date.ToString("d dddd"),
                        value = (await _blogSubscriberService.GetAllAsync(searchWeekDateUser, searchWeekDateUser.AddDays(1), true)).TotalCount.ToString()
                    });

                    searchWeekDateUser = searchWeekDateUser.AddDays(1);
                }
                break;
        }

        var result = await Result<List<Object>>.SuccessAsync(resultData);
        return Ok(result);
    }

    #endregion

    #region Post

    [HttpPost("List")]
    public async Task<IActionResult> List(BaseSearchModel baseSearchModel)
    {
        var blogPosts = await _blogSubscriberService.GetAllSubscribersAsync(pageIndex: baseSearchModel.Page - 1, pageSize: baseSearchModel.PageSize);

        var model = await new BlogSubscriberListDto().PrepareToGridAsync(baseSearchModel, blogPosts, () =>
        {
            return blogPosts.ToAsyncEnumerable().SelectAwait(async plogPost => _mapper.Map<ListBlogSubscriberDto>(plogPost));
        });

        var result = await Result<BlogSubscriberListDto>.SuccessAsync(model);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Subscribe(CreateBlogSubscriberDto dto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Invalid model");

        if(string.IsNullOrEmpty(dto.EmailAddress) || !CommonHelper.IsValidEmail(dto.EmailAddress))
            return Ok(await Result.FailAsync("Please enter a real email address"));

        if (await _blogSubscriberService.ExistsAsync(dto.EmailAddress))
            return Ok(await Result.FailAsync("Email is already subscribed"));

        if (dto.EmailAddress.Length > 128)
            return Ok(await Result.FailAsync("Please use an email address with less than 128 characters"));

        if (dto.Name.Length > 64)
            return Ok(await Result.FailAsync("Please use a name with less than 64 characters"));

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

