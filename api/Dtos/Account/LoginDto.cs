using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account
{
    public class LoginDto
    {

        public required string UserName { get; set; }


        public required string Password{ get; set; }
    }
}