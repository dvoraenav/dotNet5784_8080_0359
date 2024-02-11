using BO;

namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface ITask
{
    /// <summary>
    /// ask for the task's list(with filter)
    /// </summary>
    /// <param name="filter"> the filter we want</param>
    /// <returns> the filtered list of tasks</returns>
    public IEnumerable<TaskInList> TaskList(Func<BO.TaskInList, bool>? filter = null);
    /// <summary>
    /// build a task's object
    /// </summary>
    /// <param name="id"> the identity of task</param>
    /// <returns> object of task we built</returns>
    public BO.Task? ReadTask(int id);
    /// <summary>
    /// check propriety of id and nickname and add dependencies for privious tasks from the task's list and if 
    /// the data is ok will try to add the task to dal
    /// </summary>
    /// <param name="item"> objectof task</param>
    /// <returns> the id of task</returns>
    public int Create(BO.Task item);
    /// <summary>
    /// check the data and try to update
    /// </summary>
    /// <param name="item"> object of task that includes the update</param>
    public void Update(BO.Task item);
    /// <summary>
    /// check  if we can delete the task:if the task is exist or does not before another task and if its ok we will try to delete
    /// we cant delete tasks after create the Schedule of the project
    /// </summary>
    /// <param name="id">the identity of the task</param>
    public void Delete(int id);
    /// <summary>
    /// check if all the starting date of the privious tasks are exist and throw an eror if not.
    /// also check if the date(par) is not before all the ending data of the privious tasks
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    public void UpdateStartingDate(int id, DateTime date);
}

