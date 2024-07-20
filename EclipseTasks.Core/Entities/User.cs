using EclipseTasks.Core.Enums;

namespace EclipseTasks.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public AccessMode AccessMode { get; set; }
    }
}
