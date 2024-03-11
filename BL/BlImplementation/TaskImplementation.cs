using BlApi;
using BO;
using System.Data;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    /// <summary>
    /// creating a new object of entity
    /// </summary>
    /// <param name="item">the new BO object to create</param>
    /// <returns>the id of the new object</returns>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public int Create(BO.Task item)
    {
        try
        {
            InputIntegrityCheck(item);//if everything is ok we can try add task
            DO.Task _dotask = new DO.Task() //create new task
            {
                Name = item.Name,
                Description = item.Description,
                NumDays = item.NumDays,
                DifficultyLevel = (DO.EngineerExpireance)item.DifficultyLevel,
                Comment = item.Comment,
                NewTask = item.NewTask
            };

            int id = _dal.Task.Create(_dotask);//keep the id of the task

            if (item.Depndencies != null) // if the list of the dependancy is not empty
                item.Depndencies.ForEach(x => _dal.Dependency.Create(new DO.Dependency()//go into the list of the dependent tasks
                {
                    CurrentTaskId = id,
                    LastTaskId = x.Id  //update the tasks that i dependent in them
                }));
            return id; // return the id of the task
        }
        catch (DO.DalAlreadyExistsException ex)
        { throw new BO.BlAlreadyExistsException($"Task with ID {item.Id} already exists", ex); }
        catch(BO.BlInvalidInputPropertyException ex)
        { throw new BO.BlInvalidInputPropertyException(ex.Message); }

    }

    /// <summary>
    /// Deleting an entity object
    /// </summary>
    /// <param name="id">the id of the object to delete </param>
    /// <exception cref="BO.BlCantBeEraseException">the object can not be erased</exception>
    /// <exception cref="BO.BlDoesNotExistException">the object dose not exsit</exception>
    public void Delete(int id)
    {

        if (_dal.Dependency.ReadAll(x => x.LastTaskId == id).Any())//if there is any task that dependent on me(befor other tasks) then i can't delete the task
            throw new BO.BlCantBeEraseException("The task cannot be deleted The task takes precedence over other tasks");
        if (_dal.EndDate != null)// if we create the end date of the project then we can't delete the task
            throw new BO.BlCantBeEraseException("The task cannot be deleted after the project schedule has been created.");
        try
        {
            _dal.Task.Delete(id); //try to delete the task
        }
        catch (Exception ex)
        { throw new BO.BlDoesNotExistException(ex.Message); }

    }

    /// <summary>
    /// Returning an entity object
    /// </summary>
    /// <param name="id">the id of the object to return</param>
    /// <returns>the object </returns>
    /// <exception cref="BO.BlDoesNotExistException">if the object dose not exsite</exception>
    public BO.Task? ReadTask(int id)
    {
        DO.Task _doTask = _dal.Task.Read(x => x.Id == id) ?? throw new BO.BlDoesNotExistException($"Task with ID {id} does not exists");//look for the task with this id
        DO.Engineer? engineer = _dal.Engineer.Read(x => x.Id == _doTask.EngineerId);//look for the engineer of the task
        IEnumerable<DO.Dependency> dependencies = _dal.Dependency.ReadAll(x => x.CurrentTaskId == id);//collection whis the current tasks

        return new BO.Task()
        {
            Id = _doTask.Id,    
            Name = _doTask.Name,
            Description = _doTask.Description,
            Result = _doTask.Result,
            NumDays = _doTask.NumDays,
            NewTask = _doTask.NewTask,
            Status = SetStatus(_doTask),
            Comment = _doTask.Comment,
            DifficultyLevel = (BO.EngineerExpireance)_doTask.DifficultyLevel,
            ScheduleStart = _doTask.ScheduleStart,
            StartTask = _doTask.StartTask,
            EndTask = _doTask.EndTask,
            ForecastDate = CalcMaxDate(_doTask.StartTask, _doTask.ScheduleStart, _doTask.NumDays),
            Engineer = engineer is null ? null : new EngineerInTask()
            {
                Name = engineer.FullName,
                Id = engineer.Id,
            },
            Depndencies = (from dep in dependencies
                           let task = _dal.Task.Read(x => x.Id == dep.LastTaskId)
                           select new BO.TaskInList()
                           {
                               Id = task.Id,
                               Name = task.Name,
                               Description = task.Description,
                               Status = SetStatus(task)
                           }).ToList()
                           
        };
    }

    /// <summary>
    /// retuning a list of the entity objects
    /// </summary>
    /// <param name="filter">Filters which objects to return</param>
    /// <returns>list of the entity objects by filter if exsit if not return all</returns>
    public IEnumerable<TaskInList> TaskList(Func<TaskInList, bool>? filter = null)
    {
        return (from task in _dal.Task.ReadAll()
                select new BO.TaskInList()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Status = SetStatus(task)
                }).Where(task => filter is null ? true : filter(task));
    }

    /// <summary>
    /// updating data of object
    /// </summary>
    /// <param name="item">>The updated object</param>
    
    public void Update(BO.Task item)
    {
        try
        {
            InputIntegrityCheck(item);
            DO.Task task = _dal.Task.Read(x => x.Id == item.Id) ?? throw new BO.BlDoesNotExistException($"Task with ID {item.Id} dose not exists");
            if (_dal.Engineer.Read(x => x.Id == item.Engineer?.Id) == null)
                throw new BO.BlDoesNotExistException($"Engeineer with ID {item.Id} does not exists");
            if (_dal.EndDate == null)//we didnt create the end date time of the task
            {
                task = task with//we can update everything
                {
                    Name = item.Name,
                    Description = item.Description,
                    NumDays = item.NumDays,
                    DifficultyLevel = (DO.EngineerExpireance)item.DifficultyLevel,
                    Comment = item.Comment,
                    NewTask = item.NewTask,
                    Result = item.Result,
                    EngineerId = item.Engineer?.Id,
                };
            }
            else
            {
                task = task with//we can update only the textual fileds after created the end date
                {
                    Name = item.Name,
                    Description = item.Description,
                    Comment = item.Comment,
                    Result = item.Result,
                    EngineerId = item.Engineer?.Id,
                };
            }
            _dal.Task.Update(task);
        }
        catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException(ex.Message); }

    }

    /// <summary>
    /// Update schedule date of an object
    /// </summary>
    /// <param name="id">the id of the object</param>
    /// <param name="date">the date to update</param>
    /// <exception cref="BO.BlEarlyDatePropertyException">the date invaild</exception>
    public void UpdateStartingDate(int id, DateTime date)
    {
        try
        {
            DO.Task task = _dal.Task.Read(x => x.Id == id) ?? throw new BO.BlDoesNotExistException($"Task with ID {id} dose not exists");
            var tasks = from dep in _dal.Dependency.ReadAll(x => x.CurrentTaskId == id)
                        let lastTask = _dal.Task.Read(x => x.Id == dep.LastTaskId)
                        let endDependTask = CalcMaxDate(lastTask.StartTask, lastTask.ScheduleStart, lastTask.NumDays)
                        where lastTask.ScheduleStart == null ||  date < endDependTask // if we didnt create the start time of  all the privios tasks
                        //or the date of the starting task is before all the ending date of the privious tasks then we throw error
                        select lastTask;
            if (tasks.Any())
                throw new BO.BlGeneralExceptionException($"The last end task date is {tasks.Max(x => CalcMaxDate(x.StartTask, x.ScheduleStart, x.NumDays))}");
            _dal.Task.Update(task = task with { ScheduleStart = date });//if everything is ok we can updete the date starting task
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }//TODO general exp
        
    }

    /// <summary>
    /// setting status of task
    /// </summary>
    /// <param name="task">the task to set status to</param>
    /// <returns> the Task Status</returns>
    private BO.TaskStatus SetStatus(DO.Task task)
    {
        if(task.Id==203)
            return BO.TaskStatus.Done; ;
        if (task.EndTask.HasValue && task.EndTask <_bl.Clock)//we finishe the task
            return BO.TaskStatus.Done;
        if (!task.ScheduleStart.HasValue)// we  didnt create starting date
            return BO.TaskStatus.Unscheduled;
        if (task.ScheduleStart.HasValue && !task.StartTask.HasValue)//we created starting date whisout start work
            return BO.TaskStatus.Scheduled;
        if (task.StartTask.HasValue && !task.EndTask.HasValue)//we started work but didnt finish=in the middle
            return BO.TaskStatus.OnTrack;
        return BO.TaskStatus.Unscheduled;

    }

    /// <summary>
    /// Checking if the object field values ​​are valid
    /// </summary>
    /// <param name="item">the object to check</param>
    /// <exception cref="BO.BlNegtivePropertyException"> if the id of task is negtive </exception>
    /// <exception cref="BO.BlNullPropertyException">if the name is empty string</exception>
    private void InputIntegrityCheck(BO.Task item)
    {
        if (item.Id <= 0)
            throw new BO.BlInvalidInputPropertyException($"Task's Id can not be negative");
        if (item.Name == "")
            throw new BO.BlInvalidInputPropertyException($"Task's name can not be negative");
    }

    /// <summary>
    /// Determining an estimated time to complete the task
    /// </summary>
    /// <param name="startTask">Time the engineer started the taskparam>
    /// <param name="ScheduleStart">A time set for the start of the task</param>
    /// <param name="numDays">The number of days the task should take</param>
    /// <returns>the estimated time to complete the task</returns>
    private DateTime? CalcMaxDate(DateTime? startTask, DateTime? ScheduleStart, TimeSpan? numDays)
       => startTask > ScheduleStart ? startTask + numDays : ScheduleStart + numDays;
}

