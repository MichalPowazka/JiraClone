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
    public int AddTask(Core.Models.Task task, long projectId)
    {
        return 1;
    }

}

