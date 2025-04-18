﻿using Core.Commands.Project.CreateProject;
using Core.Commands.Project.DeleteProject;
using Core.Commands.Project.UpdateProject;
using Core.Models;
using Core.Querries.Project.GetProject;
using Core.Repostiories.Projects;
using Core.Repostiories.Sprints;
using Core.Services.AppConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JiraClone.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController(IProjectRepository projectRepository, ISprintRepository sprintRepository, IMediator mediatr) : ControllerBase
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ISprintRepository _sprintRepository = sprintRepository;
    private readonly IMediator _mediatr = mediatr;


    [HttpPost("get-project")]
    public async Task<Project> GetProject(GetProjectQuerry request)
    {
        return await _mediatr.Send(request); 
    }

    [HttpGet("get-all")]
    public List<Project> GetAll()
    {

        return _projectRepository.GetAll();

    }

    [HttpDelete]
    public async Task<long> DeleteProject(DeleteProjectCommand request)
    {

        return await _mediatr.Send(request);
    }

    [HttpPost]
    public async Task<int> AddProject(CreateProjectComand request)
    {
        return await _mediatr.Send(request);
    }

    [HttpPut]
    public async Task<long> UpdateProject(UpdateProjectCommand request)
    {
        return await _mediatr.Send(request);

    }

    [HttpPost("add-sprint")]
    public long AddSprint(Sprint sprint)
    {
        return _sprintRepository.AddSprint(sprint);
    }


    [HttpPost("add-task")]
    public long AddTask(Core.Models.Task task, long projectId)
    {
        return _projectRepository.AddTask(task, projectId);
    }

    [HttpPost("add-member")]
    public long AddTask(long userId, long projectId, long roleId)
    {
        return _projectRepository.AddMember(userId, projectId, roleId);
    }

}

