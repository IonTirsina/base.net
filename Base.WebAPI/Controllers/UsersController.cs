using Base.Application.Contexts.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Base.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Ok(id);
    }
}
