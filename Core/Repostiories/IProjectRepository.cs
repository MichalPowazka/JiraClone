using Core.Common;
using Core.Filters;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repostiories
{
    public interface IProjectRepository
    {
        Project GetProject(int id);
        List<Project> GetAll();

        List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter);
        void DeleteProject(int id);
        void AddProject(Project project);
        void UpdateProject(Project project);    
    }
}
