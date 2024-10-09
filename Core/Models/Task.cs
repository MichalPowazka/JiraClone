using Core.Common;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Task : BaseEntity<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public TaskType Type { get; set; }
    public int AssignedUserId { get; set; }
    //public User AssignedUser {get; set;}
    public int ProjectId { get; set; }
    public int EstimatedTime { get; set; }
    public int TrackedTime { get; set; }
    public int? SprintId { get; set; }

    [JsonIgnore]
    public Project? Project { get; set; }

    public int? ParentTaskId { get; set; }
    public Task? ParentTask { get; set; }

}

public enum TaskType
{
    Bug,
    Story,
    Feature
}
