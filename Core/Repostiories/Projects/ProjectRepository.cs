using Core.Common;
using Core.Filters;
using Core.Models;
using Core.Repostiories.Sprints;
using Core.Services.AppConfig;
using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Task = Core.Models.Task;


namespace Core.Repostiories.Projects;

public class ProjectRepository : IProjectRepository
{
    private readonly IAppConfigService _appConfigService;
    private readonly ISprintRepository sprintRepository;



    public ProjectRepository(IAppConfigService appConfigService, ISprintRepository sprintRepository)
    {
        _appConfigService = appConfigService;
        this.sprintRepository = sprintRepository;
    }

    public long AddMember(long userId, long projectId, long roleId)
    {
        throw new NotImplementedException();
    }

    public long AddSprint(Sprint sprint, long projectId)
    {
        throw new NotImplementedException();
    }

    public List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter)
    {
        List<Project> result;

        var whereData = string.Empty;
        var param = new DynamicParameters();
        if (!string.IsNullOrEmpty(filter.Filter.Name))
        {
            whereData += $"AND Name like @Name ";
            param.Add("Name", $"%{filter.Filter.Name}%");
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

    async Task<int> IProjectRepository.AddProject(Project project)
    {
        var sql = @"Insert Into Project (Name,StartDate,EndDate)
                    Output Inserted.Id 
                    Values(@Name, @StartDate, @EndDate)";
        using ( var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            var insertedId = await connection.QuerySingleAsync<int>(sql, project);
            return insertedId;
        }
    }

    long IProjectRepository.AddTask(Task task, long projectId)
    {
        //sprawdzic czy taki proejkt istnije
        //dodac task z odpowiednim projectId
        using var connection = new SqlConnection(_appConfigService.ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var countProjectSql = "SELECT COUNT(1) FROM Project WHERE Id = @Id";
        var projectCount = connection.QuerySingle<int>(countProjectSql, new { Id = projectId }, transaction);
        if (projectCount < 1)
        {
            throw new Exception("Project Not Exist");
        }
        var addTaskSql = @"INSERT INTO [dbo].[Task]
        ([Name]
        , [Description]
        , [Type]
        , [AssignedUserId]
        , [ProjectId]
        , [ParentTaskId]
        , [SprintId])
     Output Inserted.Id 
     VALUES
           (@Name
           , @Description
           , @Type
           , @AssignedUserId
           , @ProjectId
           , @ParentTaskId
           , @SprintId)";
        var insertedId = connection.QuerySingle<int>(addTaskSql, task, transaction);
        transaction.Commit();

      

        return insertedId;
    }

    long IProjectRepository.DeleteProject(long id)
    {

        using var connection = new SqlConnection(_appConfigService.ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        var deleteTasksSql = "Delete From Task Where ProjectId = @Id";
        var affectedTaskTable = connection.Execute(deleteTasksSql, new { Id = id }, transaction);

        var deleteSprintsSql = "Delete From Sprint Where ProjectId = @Id";
        var affectedSprintTable = connection.Execute(deleteSprintsSql, new { Id = id }, transaction);


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
        string whereData = "where ";
        DynamicParameters parameters = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(filter.Filter.Name))
        {
            whereData += $"AND p.Name like @Name ";
            parameters.Add("Name", $"%{filter.Filter.Name}%");
        }
        throw new NotImplementedException();
    }

    public async Task<Project> GetProject(int id)
    {
        using var connection = new SqlConnection(_appConfigService.ConnectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            // Fetch the project
            var sqlProject = @"SELECT p.* FROM Project p WHERE p.Id = @Id;";
            var project = await connection.QueryFirstOrDefaultAsync<Project>(sqlProject, new { Id = id }, transaction);

            if (project == null)
            {
                throw new Exception($"Project with Id {id} not found.");
            }

            // Fetch the tasks and map relations
            var sqlTasks = @"
            SELECT t.*, p.*, pt.*
            FROM Task t
            INNER JOIN Project p ON t.ProjectId = p.Id
            LEFT JOIN Task pt ON t.ParentTaskId = pt.Id
            WHERE t.ProjectId = @Id;";

            var tasks = (await connection.QueryAsync<Task, Project, Task, Task>(
                sqlTasks,
                (task, proj, parentTask) =>
                {
                    task.Project = proj;
                    task.ParentTask = parentTask;
                    return task;
                },
                new { Id = id },
                transaction
            )).ToList();

            // Fetch sprints
            var sqlSprints = @"SELECT s.* FROM Sprint s WHERE s.ProjectId = @Id;";
            var sprints = connection.Query<Sprint>(sqlSprints, new { Id = id }, transaction).ToList();

            // Assign tasks to their respective sprints
            foreach (var sprint in sprints)
            {
                sprint.Tasks = tasks.Where(t => t.SprintId == sprint.Id).ToList();
            }

            // Assign sprints and tasks to the project
            project.Sprints = sprints;
            project.Tasks = tasks;

            // Commit transaction
            transaction.Commit();

            return project;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            // Log the exception here (e.g., using a logging framework)
            throw new Exception("Error fetching project data.", ex);
        }
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

    public bool IsProjectExist(int id)
    {
        var sql = @"Select Count(*) from Project where Id = @Id";

        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            var count = connection.QuerySingle<int>(sql, new { Id = id });
            return count > 0;
        }
    }
}

//Zrbobic fabryke do connection
