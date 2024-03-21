using System.ComponentModel.DataAnnotations.Schema;

namespace BobsTacosBackend.Response
{
    public class AuthResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Id { get; internal set; }
    }
}
