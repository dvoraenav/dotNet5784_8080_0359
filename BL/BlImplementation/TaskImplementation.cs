using BlApi;
using BO;
using DO;
using System.Data;
using System.Threading.Tasks;

namespace BlImplementation;

public class TaskImplementation : ITask
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

                item.Depndencies.ForEach(x => AddDependency(item.Id, x.Id));

            return id; // return the id of the task
        }
        catch (DO.DalAlreadyExistsException ex)
        { throw new BO.BlAlreadyExistsException($"Task with ID {item.Id} already exists", ex); }
        catch (BO.BlInvalidInputPropertyException ex)
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
            if (item.Engineer.Id != 0 && task.EngineerId != null && task.EngineerId != item.Engineer.Id)
                throw new BO.BlInvalidInputPropertyException("the task already conect to other engineer");


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

                IEnumerable<DO.Dependency?> dependencies = _dal.Dependency.ReadAll(x => x.CurrentTaskId == item.Id) ?? new List<DO.Dependency>();

                if (item.Depndencies is null)
                    return;

                item.Depndencies.Where(_new => !dependencies.Any(old => old!.LastTaskId == _new.Id)).ToList().
                ForEach(_new => AddDependency(item.Id, _new.Id));//add new depenency

                dependencies.Where(old => !item.Depndencies.Any(_new => _new.Id == old!.LastTaskId)).ToList().
                    ForEach(old => _dal.Dependency.Delete(old!.Id));
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
                    StartTask = item.StartTask,
                    EndTask = item.EndTask,
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
                        where lastTask.ScheduleStart == null || date < endDependTask // if we didnt create the start time of  all the privios tasks
                        //or the date of the starting task is before all the ending date of the privious tasks then we throw error
                        select lastTask;
            if (tasks.Any())
                throw new BO.BlGeneralExceptionException($"The last end task date is {tasks.Max(x => CalcMaxDate(x.StartTask, x.ScheduleStart, x.NumDays))}");
            _dal.Task.Update(task = task with { ScheduleStart = date });//if everything is ok we can updete the date starting task
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }//TODO general exp

    }

    public IEnumerable<BO.TaskInGantt> GanttList()
    {
        return (from task in _dal.Task.ReadAll()
                select new BO.TaskInGantt()
                {
                    Id = task.Id,
                    Name = task.Name!,
                    StartOffset = (int)(task.ScheduleStart - _dal.StartDate)!.Value.TotalDays,
                    TaskLenght = (int)task.NumDays!.Value.TotalDays,
                    Status = SetStatus(task),
                    CompliteValue = CalcValue(task)
                }).OrderBy(T => T.StartOffset).ThenBy(t => t.TaskLenght);
    }

    public IEnumerable<TaskInList> TaskForEngineer(int id)
    {
        DO.Engineer eng = _dal.Engineer.Read(x => x.Id == id)!;

        IEnumerable<DO.Task> tasks = _dal.Task.ReadAll(x => x.EngineerId is null && x.DifficultyLevel <= eng.Level);

        return from task in tasks
               let boTask = ReadTask(task.Id)
               where boTask.Depndencies is null || boTask.Depndencies.Any(dep => dep.Status != BO.TaskStatus.Done)
               select new TaskInList()
               {
                   Id = task.Id,
                   Description = task.Description,
                   Name = task.Name!,
                   Status = boTask.Status
               };
    }
    private void AddDependency(int currentTaskId, int lastTaskId)
    {
        if (CheckCircularDependency(currentTaskId, lastTaskId))
            throw new BlInvalidInputPropertyException("The selected dependent tasks create a circular dependency");

        _dal.Dependency.Create(new DO.Dependency
        {
            CurrentTaskId = currentTaskId,
            LastTaskId = lastTaskId
        });
    }
    private bool CheckCircularDependency(int id1, int taskB) 
    {
        BO.Task task1 = _bl.Task.ReadTask(id1)!;
        if (task1.Depndencies == null)
            return false;
        foreach (var dependency in task1.Depndencies)
        {
            if (dependency.Id == taskB)
                return true;

            if (CheckCircularDependency(dependency.Id, taskB))
                return true;

        }
        return false;

    }
    public void ScheduleTasks(DateTime startDate)
    {
        Dictionary<int, DO.Task> tasks = _dal.Task.ReadAll().ToList().ToDictionary(task => task.Id);
        List<Dependency> dependencies = _dal.Dependency.ReadAll().ToList();


        // Initialize the schedule with tasks that have no dependencies
        Dictionary<int, DO.Task> schedule = tasks.Where(task => !dependencies.Any(dep => dep.CurrentTaskId == task.Key)).
            Select(task => task.Value).ToList().ToDictionary(task => task.Id);

        foreach (int key in schedule.Keys)
        {
            DO.Task old = schedule[key];
            old = old with { ScheduleStart = startDate };
            schedule[key] = old;
            tasks.Remove(key);
        }

        while (tasks.Count > 0)
        {
            foreach (int newTask in tasks.Keys)
            {
                bool canSchedule = true;

                foreach (Dependency dep in dependencies.Where(dep => dep.CurrentTaskId == newTask))
                {
                    if (!schedule.ContainsKey(dep.LastTaskId))
                    {
                        canSchedule = false;
                        break;
                    }
                }

                if (canSchedule)
                {
                    DateTime? earlyStart = DateTime.MinValue;
                    DateTime? lastDepDate = DateTime.MinValue;

                    foreach (Dependency dep in dependencies.Where(dep => dep.CurrentTaskId == newTask))
                    {
                        lastDepDate = schedule[dep.LastTaskId].ScheduleStart + schedule[dep.LastTaskId].NumDays;
                        if (lastDepDate > earlyStart)
                            earlyStart = lastDepDate;
                    }
                    tasks[newTask] = tasks[newTask] with { ScheduleStart = earlyStart };

                    schedule.Add(newTask, tasks[newTask]);
                    tasks.Remove(newTask);
                }
            }
        }

        schedule.Values.ToList().ForEach(task => { _dal.Task.Update(task); });
        _dal.StartDate = startDate;
        _dal.EndDate = schedule.Values.Max(t => t.ScheduleStart + t.NumDays);
    }
    private int CalcValue(DO.Task task)
    {
        if (task.StartTask is null)
            return 0;

        DateTime clock = Factory.Get.Clock;
        if (clock > task.StartTask && task.EndTask is null)
            return (int)((double)(clock - task.StartTask).Value.TotalDays / (double)task.NumDays!.Value.TotalDays) * 100;

        return 0;
    }

    /// <summary>
    /// setting status of task
    /// </summary>
    /// <param name="task">the task to set status to</param>
    /// <returns> the Task Status</returns>
    private BO.TaskStatus SetStatus(DO.Task task)
    {
        return task.ScheduleStart is null ? BO.TaskStatus.Unscheduled :
               task.StartTask is null ? BO.TaskStatus.Scheduled :
               task.EndTask is null ? BO.TaskStatus.OnTrack :
               task.EndTask < _bl.Clock ? BO.TaskStatus.Late :
               BO.TaskStatus.Done;

    }

    /// <summary>
    /// Checking if the object field values ​​are valid
    /// </summary>
    /// <param name="item">the object to check</param>
    /// <exception cref="BO.BlNegtivePropertyException"> if the id of task is negtive </exception>
    /// <exception cref="BO.BlNullPropertyException">if the name is empty string</exception>
    private void InputIntegrityCheck(BO.Task item)
    {
        if (!BO.Tools.IsAllLetters(item.Name!))
            throw new BO.BlInvalidInputPropertyException($"Task's name only contins letters");
        if (item.Name == "")
            throw new BO.BlInvalidInputPropertyException($"Task's name can not be empty");
        if (item.ScheduleStart < _dal.StartDate)
            throw new BO.BlInvalidInputPropertyException($"Schedule date can't be earlier then {_dal.StartDate}");
        if (item.EndTask < item.StartTask)
            if (item.StartTask < item.ScheduleStart)
                throw new BO.BlInvalidInputPropertyException($"Starting date can't be earlier then {item.ScheduleStart}");
        if (item.EndTask < item.StartTask)
            throw new BO.BlInvalidInputPropertyException($"Ending date can't be earlier then {item.StartTask}");

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

