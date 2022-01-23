using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Messages;
using Portfolio.Core.Services.QueuedEmails;
using Portfolio.Core.Services.Settings;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Messages;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Settings;
using Portfolio.Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    #region Fields

    private readonly ILogger<MessagesController> _logger;
    private readonly IMessageService _messageService;
    private readonly IWebHelper _webHelper;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public MessagesController(ILogger<MessagesController> logger, IMessageService messageService, IWebHelper webHelper, IMapper mapper)
    {
        _logger = logger;
        _messageService = messageService;
        _webHelper = webHelper;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var messages = (await _messageService.Get()).ToListResult();

        var result = _mapper.Map<ListResult<MessageDto>>(messages);
        result.Succeeded = true;
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateMessageDto messageDto)
    {
        var ipAddress = _webHelper.GetCurrentIpAddress();

        if (!await _messageService.IsAllowed(ipAddress))
            return Ok(Result.Fail("Please wait 2 minuted between messages"));

        var message = new Message
        {
            CreatedAtUTC = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow,
            IpAddress = ipAddress,
            Email = messageDto.Email,
            FirstName = messageDto.FirstName,
            LastName = messageDto.LastName,
            MessageContent = messageDto.MessageContent,
            MessageStatus = Domain.Enums.MessageStatus.Unread,
            HasSentNotification = false,
            IsDeleted = false
        };

        await _messageService.Create(message);
        return Ok(Result.Success("Created the message"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMessageStatus(UpdateMessageStatusDto model)
    {
        var message = await _messageService.GetById(model.Id);
        if (message == null)
            return Ok(await Result.FailAsync("Message not found"));
        

        await _messageService.UpdateMessageStatus(message, model.MessageStatus);

        var result = await Result<MessageDto>.SuccessAsync(_mapper.Map<MessageDto>(message));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await _messageService.GetById(id);
        if (message == null)
            return Ok(await Result.FailAsync("Message not found"));

        await _messageService.Delete(message);
        return Ok(await Result.SuccessAsync("Message removed"));
    }

    #endregion
}

