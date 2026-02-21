using Base.Application.Repositories;
using Base.Domain.Common;
using Base.Domain.Entities;
using Base.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;

namespace Base.Application.Contexts.Users.Commands;

public record CreateUserCommand(string Name, string Email, string Password, string PasswordConfirmation)
    : IRequest<Result<Guid, ErrorCode>>;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, Result<Guid, ErrorCode>>
{
    public async Task<Result<Guid, ErrorCode>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        return await Result.Success<string, ErrorCode>(request.Email)
            .Ensure(_ => request.Password == request.PasswordConfirmation,
                ErrorCode.PasswordNotMatch)
            .Bind(Email.Create)
            .Bind(async email =>
            {
                var existing = await unitOfWork.UserWriteRepository.GetByEmailAsync(email.Value, ct);
                return existing.HasValue
                    ? Result.Failure<Email, ErrorCode>(ErrorCode.UserEmailTaken)
                    : Result.Success<Email, ErrorCode>(email);
            })
            .Bind(email =>
                UserIdentity.CreateFromAuth0("Test_123")
                    .Map(identity => (email, identity)))
            .Bind(t => User.Create(request.Name, t.email, t.identity))
            .Tap(async user =>
            {
                await unitOfWork.UserWriteRepository.AddAsync(user, ct);
                await unitOfWork.SaveChangesAsync(ct);
            })
            .Map(user => user.Id);
    }
}
