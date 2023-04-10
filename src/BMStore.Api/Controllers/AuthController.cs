using BMStore.Application.Commands;
using BMStore.Application.Models;
using BMStore.Application.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BMStore.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
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

    [AllowAnonymous]
    [HttpGet("google")]
    public async Task<IActionResult> AuthenticateGoogleAsync(string? returnUrl)
    {
        var query = new AuthenticateGoogleQuery(returnUrl);

        var result = await _mediator.Send(query);

        return Challenge(result.Properties, result.Provider);
    }

    [AllowAnonymous]
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback(CancellationToken cancellationToken)
    {
        var command = new AuthenticateGoogleCommand();

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}
