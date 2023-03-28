using DAL.Entities;
using Services.Authentication;

namespace Services.Models.Response
{
    public class UserResponseModel
    {
        public User User { get; set; }
        public AuthenticationInformation AuthenticationInformation { get; set; }
    }
}