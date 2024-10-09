using Core.Models;
using Core.Repostiories.Projects;
using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(IProjectRepository projectRepository) : ControllerBase
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    [HttpGet]
    public Project GetProject(int id)
    {
        var p = new Project();
        p.d
        return _projectRepository.GetProject(id);

    }

    [HttpDelete]
    public int DeleteProject(int id)
    {
        
        return _projectRepository.DeleteProject(id);
    }
}

//addpoject,
//updateproject
