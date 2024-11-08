using Core.Common;
using Core.Filters;
using Core.Models;
using Core.Services.AppConfig;
using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Task = Core.Models.Task;

namespace Core.Repostiories.Projects;

public class ProjectRepository : IProjectRepository
{
    private readonly IAppConfigService _appConfigService;


    public ProjectRepository(IAppConfigService appConfigService)
    {
        _appConfigService = appConfigService;
    }



    public List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter)
    {
        List<Project> result;

        var whereData = string.Empty;
        var param = new DynamicParameters();
        if (!string.IsNullOrEmpty(filter.FIlter.Name))
        {
            whereData += $"AND Name like @Name ";
            param.Add("Name", $"%{filter.FIlter.Name}%");
        }

        var regex = new Regex(Regex.Escape("AND"));
        whereData = regex.Replace(whereData, "WHERE", 1);

        var sql = @$"Select * From Project {whereData} Order By {filter.Sort} {filter.Paging}";
        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            result = connection.Query<Project>(sql, param).ToList();
        }

        return result;

    }

    int IProjectRepository.AddProject(Project project)
    {
        var sql = @"Insert Into Project (Name,StartDate,EndDate)
                    Output Inserted.Id 
                    Values(@Name, @StartDate, @EndDate)";
        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            var insertedId = connection.QuerySingle<int>(sql, project);
            return insertedId;
        }
    }

    long IProjectRepository.AddTask(Task task, long projectId)
    {
        //sprawdzic czy taki proejkt istnije
        //dodac task z odpowiednim projectId

        throw new NotImplementedException();
    }

    int IProjectRepository.DeleteProject(int id)
    {

        using var connection = new SqlConnection(_appConfigService.ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        var deleteTasksSql = "Delete From Task Where ProjectId = @Id";
        /*Utworzenie trasakcji projektu która usuwa zadania z projektu aby zachowac spojnosc */
        //otworzyc tranzakacje
        //usunac taski z proejktu
        var affectedTaskTable = connection.Execute(deleteTasksSql, new { Id = id }, transaction);

        var deleteProjectSql = @"Delete From Project Where Id = @Id";

        var affectedProjectTable = connection.Execute(deleteProjectSql, new { Id = id }, transaction);
        transaction.Commit();
        connection.Close();


        return affectedProjectTable;
        // tranakcje skomplitowac

    }

    List<Project> IProjectRepository.GetAll()
    {
        List<Project> result;
        var sql = @"Select * From Project";
        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            result = connection.Query<Project>(sql).ToList();
        }

        return result;
    }

    List<Project> IProjectRepository.GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter)
    {
        throw new NotImplementedException();
    }

    Project IProjectRepository.GetProject(int id)
    {
        Project result;
        var sql = @"Select t.*, p.*, pt.* From Task t
                    inner join Project p on t.ProjectId = p.Id
                    left join Task pt on t.ParentTaskId = pt.Id 
                    where t.ProjectId = @Id";
        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            var tasks = connection.Query<Task, Project, Task, Task>(sql,
               (t, p, pt) =>
               {
                   t.Project = p;
                   t.ParentTask = pt;
                   return t;
               }, new { Id = id }).ToList();

            sql = @"Select t.*, s.* From Task t
                    inner join Sprint s on t.SprintId = s.Id
                    where s.ProjectId = @Id;";
            result = tasks.First().Project!;

            var sprints = connection.Query<Task, Sprint, Task>(sql,
                (task, sprint) =>
                {
                    task.Sprint = sprint;
                    return task;
                },
                new { Id = id })
                .ToList()
                .GroupBy(t => t.Sprint)
                .Select(g =>
                {
                    var sprint = g.Key;
                    sprint!.Tasks = g.ToList();
                    return sprint;
                })
                .ToList();

            result.Sprints = sprints;

            //zapakowac to w tranazacje


            //zrobic tranazakcje, i dociągnac sprinty na podstwie project id
            result.Tasks = tasks;
        }


        return result;
    }

    int IProjectRepository.UpdateProject(Project project)
    {

        var sql = @"Update Project SET Name = @Name,  StartDate = @StartDate,  EndDate = @EndDate Where Id = @Id";

        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            connection.Execute(sql, project);
            return project.Id;
        }
    }
}

//Zrbobic fabryke do connection
