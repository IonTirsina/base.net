using Base.Application.Contexts.Users.Commands;
using Base.Domain.Common;
using Base.WebAPI.Models;
using CSharpFunctionalExtensions;
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
        var idResult = await sender.Send(command, cancellationToken);

        return idResult.IsSuccess ? Ok(ApiResponse<Guid>.Ok(idResult.Value)) : Ok(ApiResponse<ErrorCode>.Failure(idResult.Error));
    }
}
