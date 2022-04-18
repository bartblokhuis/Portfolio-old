using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Dtos.Authentication;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Users;
using Portfolio.Services.WorkContexts;
using System;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    #region Fields

    private readonly IWorkContext _workContext;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public UserController(IWorkContext workContext, IMapper mapper)
    {
        _workContext = workContext;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    [Route("Current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var currentUser = await _workContext.GetCurrentUserAsync();

        if (currentUser == null)
            throw new ApplicationException("No logged in user found");

        var dto = _mapper.Map<ApplicationUserDto>(currentUser);

        return Ok(await Result<ApplicationUserDto>.SuccessAsync(dto));
    }

    #endregion

    #endregion
}
