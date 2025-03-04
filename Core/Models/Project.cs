using Core.Common;

namespace Core.Models;

public class Project : BaseEntity<int>
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public List<Task>? Tasks { get; set; } = [];
    public List<Sprint>? Sprints { get; set; } = [];
    public List<User>? Member  { get; set; } = [];

}


