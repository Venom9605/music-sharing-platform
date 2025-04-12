using Base.Domain.Identity;
using Base.Interfaces;

namespace Domain.Identity;

public class AppRefreshToken : BaseRefreshToken, IDomainUserId
{
    public string UserId { get; set; } = default!;
    public Artist? User { get; set; } 
}