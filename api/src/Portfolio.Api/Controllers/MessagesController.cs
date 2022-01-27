using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Messages;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Common;
using Portfolio.Services.Messages;
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

    #region Utils

    private string Validate(CreateMessageDto dto)
    {
        if (dto == null)
            return "Unkown error";

        if (dto.FirstName?.Length > 64)
            return "Please don't enter a name with more than 64 character";

        if (dto.LastName?.Length > 64)
            return "Please don't enter a name with more than 64 character";

        if (string.IsNullOrEmpty(dto.Email))
            return "Please enter your email address";

        if (string.IsNullOrEmpty(dto.MessageContent))
            return "Please enter your message";

        if (dto.MessageContent.Length > 512)
            return "Please don't use more than 512 charachters in your message";

        if (dto.Email.Length > 64)
            return "";

        return "";
    }

    #endregion

    #region Methods

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var messages = (await _messageService.GetAllAsync()).ToListResult();

        var result = _mapper.Map<ListResult<MessageDto>>(messages);
        result.Succeeded = true;
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateMessageDto messageDto)
    {
        var error = Validate(messageDto);
        if(!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var ipAddress = _webHelper.GetCurrentIpAddress();

        if (!await _messageService.IsAllowedAsync(ipAddress))
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

        await _messageService.InsertAsync(message);
        return Ok(Result.Success("Created the message"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMessageStatus(UpdateMessageStatusDto model)
    {
        var message = await _messageService.GetByIdAsync(model.Id);
        if (message == null)
            return Ok(await Result.FailAsync("Message not found"));
        

        await _messageService.UpdateMessageStatusAsync(message, model.MessageStatus);

        var result = await Result<MessageDto>.SuccessAsync(_mapper.Map<MessageDto>(message));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await _messageService.GetByIdAsync(id);
        if (message == null)
            return Ok(await Result.FailAsync("Message not found"));

        await _messageService.DeleteAsync(message);
        return Ok(await Result.SuccessAsync("Message removed"));
    }

    #endregion
}

