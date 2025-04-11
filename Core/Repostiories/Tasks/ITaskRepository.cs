namespace Core.Repostiories.Tasks
{
    public interface ITaskRepository
    {
        Task<long> CreateTask(Task task);

        Task<long> UpdateTask(Task task);

        Task<Models.Task> GetTaskById(int id);

        Task<Models.Task> GetProjectTask(int projectId, int taskId);
    }
}
