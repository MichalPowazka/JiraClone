using Core.Common;
using Core.Filters;
using Core.Models;

namespace Core.Repostiories.Projects;

public interface IProjectRepository
{
    Project GetProject(int id);
    List<Project> GetAll();

    List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter);
    int DeleteProject(int id);
    void AddProject(Project project);
    void UpdateProject(Project project);
}
