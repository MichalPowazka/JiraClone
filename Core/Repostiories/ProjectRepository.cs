using Core.Common;
using Core.Filters;
using Core.Models;
using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Core.Repostiories;

public class ProjectRepository : IProjectRepository
{
    public List<Project> GetPagedFilteredList(PagedQuerryFilter<ProjectFilter> filter)  
    {
        List<Project> result;

        var whereData = string.Empty;
        var param = new DynamicParameters();
        if(!string.IsNullOrEmpty(filter.FIlter.Name) )
        {
            whereData += $"AND Name like @Name ";
            param.Add("Name", $"%{filter.FIlter.Name}%");
        }

        var regex = new Regex(Regex.Escape("AND"));
        whereData = regex.Replace(whereData, "WHERE", 1);

        var sql = @$"Select * From Project {whereData} Order By {filter.Sort} {filter.Paging}";
        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            result = connection.Query<Project>(sql,param).ToList();
        }

        return result;

    }

    void IProjectRepository.AddProject(Project project)
    {
        var sql = @"Insert Into Project (Name,StartDate,EndDate)
                    Output Inserted.Id 
                    Values(@Name, @StartDate, @EndDate)";
        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            var insertedId = connection.QuerySingle<int>(sql, project);
        }
    }

    void IProjectRepository.DeleteProject(int id)
    {
        var sql = @"Delete From Project Where Id = @Id";

        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            var insertedId = connection.Execute(sql, new { Id = id });
        }

    }

    List<Project> IProjectRepository.GetAll()
    {
        List<Project> result;
        var sql = @"Select * From Project";
        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            result = connection.Query<Project>(sql).ToList();
        }

        return result;
    }

    Project IProjectRepository.GetProject(int id)
    {
        Project result;
        var sql = @"Select * From Project Where Id = @Id";
        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
            result = connection.QuerySingle<Project>(sql, new { Id = id });
        }

        return result;
    }

    void IProjectRepository.UpdateProject(Project project) { 
    
        var sql = @"Update Project SET Name = @Name,  StartDate = @StartDate,  EndDate = @EndDate Where Id = @Id";

        using (var connection = new SqlConnection("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
        {
           connection.Execute(sql, project);
        }
    }
}

//wyrzucenie connection stringu do appsetings
//Zrbobic fabryke do connection
