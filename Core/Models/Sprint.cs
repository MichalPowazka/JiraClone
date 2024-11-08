using Core.Common;
using System.Numerics;

namespace Core.Models
{
    public class Sprint : BaseEntity<int>
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>(); 
    }
}
