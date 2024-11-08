using Core.Models;
using Core.Repostiories.Sprints;
using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers;

[Route("api/sprint")]
[ApiController]
public class SprintController(ISprintRepository sprintRepository) : ControllerBase
{
    private readonly ISprintRepository _sprintRepository = sprintRepository;

    [HttpGet]
    public Sprint GetSprint(long id)
    {
        var p = new Sprint();
        return _sprintRepository.GetSprint(id);
    }

    [HttpDelete]
    public long DeleteSprint(long id)
    {
        return _sprintRepository.DeleteSprint(id);
    }

    [HttpPost]
    public int AddSprint(Sprint sprint)
    {
        return _sprintRepository.AddSprint(sprint);
    }

    [HttpPut]
    public int UpdateSprint(Sprint sprint)
    {
        return _sprintRepository.UpdateSprint(sprint);
    }

    [HttpPost("add-task")]
    public int AddTask(Core.Models.Task task, long sprintId)
    {
        return 1;
    }


}

