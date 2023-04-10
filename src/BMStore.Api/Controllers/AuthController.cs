using BMStore.Application.Commands;
using BMStore.Application.Models;
using BMStore.Infrastructure.Identity.Models;

using MediatR;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BMStore.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(IMediator mediator,
        SignInManager<ApplicationUser> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    // POST: api/Token/Authenticate
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<TokenResponse> AuthenticateAsync([FromBody] AuthenticateCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Resource;
    }

    [Authorize]
    [HttpGet("test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> Authorized()
    {
        return Ok("Authorized");
    }

    [HttpGet("google")]
    public async Task<IActionResult> AuthenticateGoogleAsync(string? returnUrl)
    {
        var provider = GoogleDefaults.AuthenticationScheme;
        AuthenticationProperties properties = PrepareExternalLoginProperties(provider, returnUrl);
        return Challenge(properties, provider);
    }

    private AuthenticationProperties PrepareExternalLoginProperties(string provider, string? returnUrl)
    {
        //TODO move this somwhere
        var redirectUrl = $"/api/auth/google-callback?returnUrl={returnUrl}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.AllowRefresh = true;
        return properties;
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback(string? returnUrl)
    {
        var command = new AuthenticateGoogleCommand();

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
