using Core.Commands.Project.AddSprint;
using Core.Models;
using Core.Services.AppConfig;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Core.Repostiories.Sprints
{
    public class SprintRepository : ISprintRepository
    {
        private readonly IAppConfigService _appConfigService;

        public SprintRepository(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }


        long ISprintRepository.AddSprint(Sprint sprint)
        {
            //sprawdzic czy projekt istinieje

            var sql = @"Insert Into Sprint (Name,Description,StartDate,EndDate,ProjectId)
                    Output Inserted.Id 
                    Values(@Name, @Description,@StartDate, @EndDate, @ProjectId)";
            using (var connection = new SqlConnection(_appConfigService.ConnectionString))
            {
                var insertedId = connection.QuerySingle<long>(sql, sprint);
                return insertedId;
            }
        }

        long ISprintRepository.AddTask(Models.Task task, long sprintId)
        {
   

            using var connection = new SqlConnection(_appConfigService.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var getSprint = @"Select * From Sprint Where Id = @SprintId";
            var sId = connection.QuerySingle(getSprint, new { SprintId = sprintId }, transaction);
            task.SprintId = sId;

            if (sId != 0)
            {

                var sql = @"Insert Into Task (Name,Description,SprintId,ParentTaskId)
                        Output Inserted.Id
                        Values(@Name,@Description,@SprintId,@ParentTaskId)";

                var taskId = connection.QuerySingle<int>(sql, task, transaction);
                return taskId;
            }


            return -1;

        }

        long ISprintRepository.DeleteSprint(long id)
        {
            using var connection = new SqlConnection(_appConfigService.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var updateTasksSql = @"Update Task Set SprintId = null Where SprintId = @Id";
            var affectedTaskTable = connection.Execute(updateTasksSql, new { Id = id }, transaction);

            var deleteSprintSql = @"Delete From Sprint Where Id = @Id";


            var affectedSprintTable = connection.Execute(deleteSprintSql, new { Id = id }, transaction);

            transaction.Commit();
            return affectedSprintTable;
        }

        List<Sprint> ISprintRepository.GetAll()
        {
            List<Sprint> result;
            var sql = @"Select * From Sprint";
            using (var connection = new SqlConnection(_appConfigService.ConnectionString))
            {
                result = connection.Query<Sprint>(sql).ToList();
            }

            return result;
        }

        Sprint ISprintRepository.GetSprint(long id)
        {
            
            var sql = @"Select * From Sprint";
            using (var connection = new SqlConnection(_appConfigService.ConnectionString))
            {
                var result = connection.QuerySingle<Sprint>(sql);
                return result;
            }
            return null;

            
        }

        long ISprintRepository.UpdateSprint(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        //Sprint ISprintRepository.GetSprint(int id)
        //{
        //    Sprint result;

        //    var sql=@"Select s.* From Sprint s where s."

        //}
    }
}
