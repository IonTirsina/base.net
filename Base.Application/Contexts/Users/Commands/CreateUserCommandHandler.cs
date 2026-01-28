using Base.Application.Repositories;
using Base.Application.Repositories.Users;
using Base.Domain.Entities;
using Base.Domain.ValueObjects;
using MediatR;

namespace Base.Application.Contexts.Users.Commands;

public record CreateUserCommand(string Name, string Email, string Password) : IRequest<Guid>;

public class CreateUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = User.Create(
            name: request.Name,
            email: Email.Create(request.Email),
            password: request.Password
        );
        await writeRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}
