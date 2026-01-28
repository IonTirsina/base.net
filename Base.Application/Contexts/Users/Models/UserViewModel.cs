using Base.Domain.Entities;

namespace Base.Application.Contexts.Users.Models;

public record UserViewModel(Guid Id, string Name, string Email)
{
    public UserViewModel(User user) : this(user.Id, user.Name, user.Email.Value)
    {
    }
}
