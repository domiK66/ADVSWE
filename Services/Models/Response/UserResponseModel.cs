using DAL.Entities;
using Services.Auth;

namespace Services.Models.Response
{
    public class UserResponseModel
    {
        public User User { get; set; }
        public AuthenticationInformation AuthInfo { get; set; }
    }
}