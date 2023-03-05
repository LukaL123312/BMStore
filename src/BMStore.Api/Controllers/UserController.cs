using BMStore.Application.Commands;
using BMStore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BMStore.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediatr;
    public UserController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetUsersQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddUsers([FromBody] AddUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        return Ok(result);
    }


}