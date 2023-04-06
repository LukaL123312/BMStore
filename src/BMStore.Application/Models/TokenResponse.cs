using BMStore.Domain.Entities;

namespace BMStore.Application.Models;

public class TokenResponse
{
    public TokenResponse(UserEntity user,
                         string role,
                         string token
                        //string refreshToken
                        )
    {
        Id = user.IdentityId;
        FullName = user.FullName;
        EmailAddress = user.Email;
        Token = token;
        Role = role;
        //RefreshToken = refreshToken;
    }

    public string Id { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }

    //[JsonIgnore]
    //public string RefreshToken { get; set; }
}
