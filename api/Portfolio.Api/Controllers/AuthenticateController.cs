using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Domain.Dtos.Authentication;
using Portfolio.Domain.Models.Authentication;
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

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region Methods

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Ok(new
                {
                    error = "Invalid username or password."
                });
            

        var token = await GetJwtSecurityToken(user, model.RememberMe);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            userId = user.Id
        });
    }

    private async Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user, bool rememberMe)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        var loginTime = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(8);

        foreach (var userRole in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));


        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        return new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: loginTime,
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }

    [HttpGet]
    [Route("user/details")]
    [Authorize()]
    public async Task<ApplicationUserDto> GetUserDetails()
    {
        var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(currentUserName))
            throw new ApplicationException("No logged in user found");

        var user = await _userManager.FindByNameAsync(currentUserName);

        if (user == null)
            throw new ApplicationException("No logged in user found");

        return new ApplicationUserDto
        {
            Email = user.Email,
            Username = user.UserName
        };
    }

    [HttpPut]
    [Route("user/details")]
    [Authorize()]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsDto model)
    {
        if (!ModelState.IsValid)
            throw new Exception("Invalid model");

        var user = await GetUserFromContext();
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Ok(new
            {
                error = "Invalid username or password."
            });

        user.UserName = model.Username;
        user.NormalizedUserName = model.Username.ToUpper();

        user.Email = model.Email;
        user.NormalizedEmail = model.Email.ToUpper();

        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpPut]
    [Route("user/updatePassword")]
    [Authorize()]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(500);
        }

        var user = await GetUserFromContext();
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.OldPassword))
            return Ok(new
            {
                error = "Invalid username or password."
            });

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
        if (!result.Succeeded)
            return Ok(new
            {
                errors = result.Errors.Select(x => x.Description)
            });

        return Ok();
    }

    #endregion

    #region Utils

    private Task<ApplicationUser> GetUserFromContext()
    {
        var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(currentUserName))
            return null;

        return _userManager.FindByNameAsync(currentUserName);
    }

    #endregion
}

