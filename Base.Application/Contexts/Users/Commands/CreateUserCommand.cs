using Base.Application.Repositories;
using Base.Domain.Common;
using Base.Domain.Entities;
using Base.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;

namespace Base.Application.Contexts.Users.Commands;

public record CreateUserCommand(string Name, string Email, string Password, string PasswordConfirmation) : IRequest<IResult<Guid, ErrorCode>>;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, IResult<Guid, ErrorCode>>
{
    public async Task<IResult<Guid, ErrorCode>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.PasswordConfirmation)
            return Result.Failure<Guid, ErrorCode>(ErrorCode.PasswordNotMatch);

        var existingUser = await unitOfWork.UserWriteRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser.HasValue)
            return Result.Failure<Guid, ErrorCode>(ErrorCode.UserEmailTaken);
        
        var userResult = User.Create(
            name: request.Name,
            email: Email.Create(request.Email),
            password: request.Password
        );

        if (!userResult.IsSuccess)
        {
            return Result.Failure<Guid, ErrorCode>(userResult.Error);
        }

        var user = userResult.Value;
        await unitOfWork.UserWriteRepository.AddAsync(userResult.Value, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success<Guid, ErrorCode>(user.Id);
    }
}
