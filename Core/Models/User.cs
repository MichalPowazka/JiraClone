using Core.Common;

namespace Core.Models
{
    public class User : BaseEntity<long>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PaswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool PaswordChangeRequired { get; set; }
    }
}
