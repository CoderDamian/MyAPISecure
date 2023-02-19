using System.ComponentModel.DataAnnotations;

namespace MyModels
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
