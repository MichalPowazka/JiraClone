using Core.Common;
using Core.Filters;
using Core.Models;
using System.ComponentModel;
using Task = Core.Models.Task;

namespace Core.Repostiories.Projects;

public interface IProjectRepository
{
    Task<Project> GetProject(int id);
    List<Project> GetAll();

    List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter);
    Task<long> DeleteProject(long id);
    Task<int> AddProject(Project project);
    Task<int> UpdateProject(Project project);
    long AddTask(Task task, long projectId);
    long AddMember(long userId, long projectId, long roleId);
    long AddSprint(Sprint sprint, long projectId);
    bool IsProjectExist(int id);

}
