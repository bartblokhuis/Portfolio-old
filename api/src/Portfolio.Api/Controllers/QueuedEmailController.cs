﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Helpers;
using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Dtos.QueuedEmails;
using Portfolio.Domain.Extensions;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.QueuedEmails;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class QueuedEmailController : ControllerBase
{
    #region Fields

    private readonly IQueuedEmailService _queuedEmailService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public QueuedEmailController(IQueuedEmailService queuedEmailService, IMapper mapper)
    {
        _queuedEmailService = queuedEmailService;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var queuedEmails = (await _queuedEmailService.SearcAsync(null, null, null, null, false, false, int.MaxValue, true)).ToListResult();

        var result = _mapper.Map<ListResult<ListQueuedEmailDto>>(queuedEmails);
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var queuedEmail = (await _queuedEmailService.GetByIdAsync(id));
        if (queuedEmail == null)
            return Ok(await Result.FailAsync("No queued email found"));

        var result = _mapper.Map<UpdateQueuedEmailDto>(queuedEmail);
        return Ok(await Result<UpdateQueuedEmailDto>.SuccessAsync(result));
    }

    #endregion

    #region Post

    [HttpPost("List")]
    public async Task<IActionResult> List(BaseSearchModel baseSearchModel)
    {
        var queuedEmails = await _queuedEmailService.GetAllQueuedEmailsAsync(pageIndex: baseSearchModel.Page - 1, pageSize: baseSearchModel.PageSize);

        var model = await new QueuedEmailListDto().PrepareToGridAsync(baseSearchModel, queuedEmails, () =>
        {
            return queuedEmails.ToAsyncEnumerable().SelectAwait(async queuedEmail => _mapper.Map<ListQueuedEmailDto>(queuedEmail));
        });

        var result = await Result<QueuedEmailListDto>.SuccessAsync(model);
        return Ok(result);
    }

    #endregion

    #region Put

    [HttpPut]
    public async Task<IActionResult> Edit(UpdateQueuedEmailDto dto)
    {
        //Validation
        if (dto == null)
            return Ok(await Result.FailAsync("Unkown error"));

        if(string.IsNullOrEmpty(dto.From))
            return Ok(await Result.FailAsync("Please enter a from email address"));

        if (dto.From.Length > 128)
            return Ok(await Result.FailAsync("Please don't enter a from address with more than 128 character"));

        if (!CommonHelper.IsValidEmail(dto.From))
            return Ok(await Result.FailAsync("Please enter a valid from email address"));

        if (!string.IsNullOrEmpty(dto.FromName) && dto.FromName.Length > 128)
            return Ok(await Result.FailAsync("Please don't enter a from name with more than 128 character"));

        if (string.IsNullOrEmpty(dto.To))
            return Ok(await Result.FailAsync("Please enter a to email address"));

        if (dto.To.Length > 128)
            return Ok(await Result.FailAsync("Please don't enter a to address with more than 128 character"));

        if (!CommonHelper.IsValidEmail(dto.To))
            return Ok(await Result.FailAsync("Please enter a valid to email address"));

        if(!string.IsNullOrEmpty(dto.ToName) && dto.ToName.Length > 128)
            return Ok(await Result.FailAsync("Please don't enter a to name with more than 128 character"));

        if (string.IsNullOrEmpty(dto.Subject))
            return Ok(await Result.FailAsync("Please enter the email subject"));

        if (!string.IsNullOrEmpty(dto.Subject) && dto.Subject.Length > 128)
            return Ok(await Result.FailAsync("Please don't enter a subject with more than 128 character"));

        if (string.IsNullOrEmpty(dto.Body))
            return Ok(await Result.FailAsync("Please enter the email content"));

        if(dto.SentTries < 0)
            return Ok(await Result.FailAsync("Please don't enter sub zero numberst"));

        if (dto.SentTries > 3)
            return Ok(await Result.FailAsync("Please don't enter a number higher than 3"));

        //Get the original queued email
        var originalEmail = await _queuedEmailService.GetByIdAsync(dto.Id);
        if (originalEmail == null)
            return Ok(await Result.FailAsync("No queued email found"));

        //Update the updated fields
        _mapper.Map(dto, originalEmail);

        //Save the changes
        await _queuedEmailService.UpdateAsync(originalEmail);

        var result = await Result<ListQueuedEmailDto>.SuccessAsync(_mapper.Map<ListQueuedEmailDto>(originalEmail));
        return Ok(result);
    }

    #endregion

    #region Delete

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var queuedEmail = await _queuedEmailService.GetByIdAsync(id);
        if (queuedEmail == null)
            return Ok(await Result.FailAsync("Queued email not found"));

        await _queuedEmailService.DeleteAsync(queuedEmail);

        var result = await Result.SuccessAsync("removed the email from the queue");
        return Ok(result);
    }

    [HttpDelete("DeleteAll")]
    public async Task<IActionResult> DeleteAll()
    {
        await _queuedEmailService.DeleteAllAsync();

        var result = await Result.SuccessAsync("removed all the queued emails");
        return Ok(result);
    }

    #endregion

    #endregion
}
