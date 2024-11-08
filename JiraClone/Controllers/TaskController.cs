using Core.Models;
using Core.Repostiories.Sprints;
using Core.Repostiories.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers;

[Route("api/task")]
[ApiController]
public class TaskController(ITaskRepository _taskRepository) : ControllerBase
{

    [HttpGet]
    public Core.Models.Task GetSprint(int id)
    {
        
        //return _taskRepository.GetSprint(id);
        return new Core.Models.Task() { Name = "TEST"};

    }

    [HttpDelete]
    public int DeleteTask(int id)
    {
        //return _taskRepository.DeleteTask(id);
        return 1;

    }


    [HttpPut]
    public int UpdateTask(Core.Models.Task task)
    {
        //return _taskRepository.UpdateSprint(task);
        return 1;

    }

    [HttpPost("add-task")]
    public int AddTask(Core.Models.Task task)
    {
        return 1;
    }


}