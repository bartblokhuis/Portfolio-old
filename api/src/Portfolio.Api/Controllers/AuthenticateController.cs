using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Core.Configuration;
using Portfolio.Domain.Dtos.Authentication;
using Portfolio.Domain.Models.Authentication;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Users;
using Portfolio.Services.WorkContexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

public class AuthenticateController : ControllerBase
{
    #region Fields

    private readonly IConfiguration _configuration;
    private readonly AppSettings _appSettings;
    private readonly IUserService _userService;
    private readonly IWorkContext _workContext;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public AuthenticateController(IConfiguration configuration, AppSettings appSettings, IUserService userService, IWorkContext workContext, IMapper mapper)
    {
        _configuration = configuration;
        _appSettings = appSettings;
        _userService = userService;
        _workContext = workContext;
        _mapper = mapper;
    }

    #endregion

    #region Utils

    private async Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user, bool rememberMe)
    {
        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        var loginTime = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(8);

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        return new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: loginTime,
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    [Route("user/details")]
    [Authorize()]
    public async Task<IActionResult> GetUserDetails()
    {
        var currentUser = await _workContext.GetCurrentUserAsync();

        if (currentUser == null)
            throw new ApplicationException("No logged in user found");

        return Ok(await Result<ApplicationUserDto>.SuccessAsync(new ApplicationUserDto
        {
            Email = currentUser.Email,
            Username = currentUser.UserName
        }));
    }

    #endregion

    #region Post

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userService.GetUserByNameOrEmail(model.Username);
        if (user == null || !await _userService.CheckPasswordAsync(user, model.Password))
            return Ok(await Result.FailAsync("Invalid username or password."));


        var token = await GetJwtSecurityToken(user, model.RememberMe);
        var response = new Response(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.Id);

        return Ok(await Result<Response>.SuccessAsync(response));
    }

    #endregion

    #region Put

    [HttpPut]
    [Route("user/details")]
    [Authorize()]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsDto model)
    {
        if (_appSettings.IsDemo)
            return Ok(await Result.FailAsync("Updating the user details is not allowed in the demo application"));

        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync("Invalid model"));

        var currentUser = await _workContext.GetCurrentUserAsync();
        if (currentUser == null || !await _userService.CheckPasswordAsync(currentUser, model.Password))
            return Ok(await Result.FailAsync("Invalid username or password."));

        currentUser.UserName = model.Username;
        currentUser.NormalizedUserName = model.Username.ToUpper();
        
        currentUser.Email = model.Email;
        currentUser.NormalizedEmail = model.Email.ToUpper();

        await _userService.UpdateAsync(currentUser);
        return Ok(await Result.SuccessAsync("User updated"));
    }

    [HttpPut]
    [Route("user/updatePassword")]
    [Authorize()]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
    {
        if (_appSettings.IsDemo)
            return Ok(await Result.FailAsync("Updating the user password is not allowed in the demo application"));

        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync("Invalid model"));
        

        var currentUser = await _workContext.GetCurrentUserAsync();
        if (currentUser == null || !await _userService.CheckPasswordAsync(currentUser, model.OldPassword))
            return Ok(await Result.FailAsync("Invalid username or password."));

        var result = await _userService.ChangePasswordAsync(currentUser, model.OldPassword, model.Password);
        if (!result.Succeeded)
            return Ok(await Result.FailAsync(result.Errors.Select(x => x.Description).ToList()));


        return Ok(await Result.SuccessAsync("Updated the password"));
    }

    #endregion

    #endregion
}

