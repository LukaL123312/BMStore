using BMStore.Application.Commands;
using BMStore.Application.Models;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BMStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/Token/Authenticate
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<TokenResponse> AuthenticateAsync([FromBody] AuthenticateCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Resource;
    }
}
