using System;
namespace CleanArchMvc.API.Models
{
    public class UserTokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }    
    }
}
