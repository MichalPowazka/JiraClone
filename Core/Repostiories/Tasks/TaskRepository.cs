using Core.Services.AppConfig;

namespace Core.Repostiories.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly IAppConfigService _appConfigService;

    public TaskRepository(IAppConfigService appConfigService)
    {
        _appConfigService = appConfigService;
    }

    Task<long> ITaskRepository.CreateTask(Task task)
    {
        throw new NotImplementedException();
    }

    Task<long> ITaskRepository.UpdateTask(Task task)
    {
        throw new NotImplementedException();
    }

    Task<Models.Task> ITaskRepository.GetTaskById(int id)
    {
        throw new NotImplementedException();
    }

    Task<Models.Task> ITaskRepository.GetProjectTask(int projectId, int taskId)
    {
        throw new NotImplementedException();
    }

}
