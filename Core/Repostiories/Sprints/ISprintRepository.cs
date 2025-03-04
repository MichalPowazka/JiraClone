using Core.Models;
using Task = Core.Models.Task;

namespace Core.Repostiories.Sprints;

public interface ISprintRepository
{

    Sprint GetSprint(long id);
    List<Sprint> GetAll();
    long DeleteSprint(long id);
    long AddSprint(Sprint sprint);
    long UpdateSprint(Sprint sprint);
    long AddTask(Task task, long sprintId);


}
