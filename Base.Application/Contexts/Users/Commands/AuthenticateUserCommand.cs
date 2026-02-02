using Base.Application.Repositories;
using MediatR;

namespace Base.Application.Contexts.Users.Commands;

public record AuthenticateUserCommand(string Email, string Password) : IRequest<string>;

public class AuthenticateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AuthenticateUserCommand, string>
{
    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult("");
    }
}
