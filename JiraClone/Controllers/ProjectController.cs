using Core.Models;
using Core.Repostiories.Projects;
using Core.Services.AppConfig;
using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController(IProjectRepository projectRepository ) : ControllerBase
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    [HttpGet]
    public Project GetProject(int id)
    {
        var p = new Project();
        return _projectRepository.GetProject(id);

    }

    [HttpGet("get-all")]
    public List<Project> GetAll()
    {
        var p = new Project();
        return _projectRepository.GetAll();

    }

    [HttpDelete]
    public long DeleteProject(long id)
    {
        
        return _projectRepository.DeleteProject(id);
    }

    [HttpPost]
    public long AddProject(Project project)
    {
        return _projectRepository.AddProject(project);
    }
    [HttpPut]
    public long UpdateProject(Project project)
    {
        return _projectRepository.UpdateProject(project);

    }

    [HttpPost("add-sprint")]
    public long AddSprint(Sprint sprint)
    {
        return 1;
    }


    [HttpPost("add-task")]
    public long AddTask(Core.Models.Task task, long projectId)
    {
        return _projectRepository.AddTask(task,projectId);
    }

    [HttpPost("add-member")]
    public long AddTask(long UserId, long projectId, long RoleId)
    {
        return _projectRepository.AddTask(task, projectId);
    }

}

