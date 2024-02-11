using BlApi;
using BO;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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

    }

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
                           let task = _dal.Task.Read(x => x.Id == dep.CurrentTaskId)
                           select new BO.TaskInList()
                           {
                               Id = task.Id,
                               Name = task.Name,
                               Description = task.Description,
                               Status = SetStatus(task)
                           }).ToList()
        };
    }

    public IEnumerable<TaskInList> ReadAll(Func<TaskInList, bool>? filter = null)
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

    public void Update(BO.Task item)
    {
        try
        {
            InputIntegrityCheck(item);
            DO.Task task = _dal.Task.Read(x => x.Id == item.Id) ?? throw new BO.BlAlreadyExistsException($"Task with ID {item.Id} already exists");
            if (_dal.Engineer.Read(x => x.Id == item.Engineer.Id) == null)
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
        catch (DO.DalAlreadyExistsException ex) { throw new BO.BlNullPropertyException(ex.Message); }

    }

    public void UpdateStartingDate(int id, DateTime date)//update the date to start the task
    {
        try
        {
            DO.Task task = _dal.Task.Read(x => x.Id == id) ?? throw new BO.BlAlreadyExistsException($"Task with ID {id} already exists");
            var tasks = from dep in _dal.Dependency.ReadAll(x => x.CurrentTaskId == id)
                        let lastTask = _dal.Task.Read(x => x.Id == dep.LastTaskId)
                        let endDependTask = CalcMaxDate(lastTask.StartTask, lastTask.ScheduleStart, lastTask.NumDays)
                        where lastTask.ScheduleStart == null ||  date < endDependTask // if we didnt create the start time of  all the privios tasks
                        //or the date of the starting task is before all the ending date of the privious tasks then we throw error
                        select lastTask;
            if (tasks.Any())
                throw new BO.BlEarlyDatePropertyException($"The last end task date is {tasks.Max(x => CalcMaxDate(x.StartTask, x.ScheduleStart, x.NumDays))}");
            _dal.Task.Update(task = task with { ScheduleStart = date });//if everything is ok we can updete the date starting task
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }//TODO general exp

    }
    private BO.TaskStatus SetStatus(DO.Task t)
    {
        if (t.EndTask.HasValue && t.EndTask < DateTime.Now)//we finishe the task
            return BO.TaskStatus.Done;
        if (!t.ScheduleStart.HasValue)// we  didnt create starting date
            return BO.TaskStatus.Unscheduled;
        if (t.ScheduleStart.HasValue && !t.StartTask.HasValue)//we created starting date whisout start work
            return BO.TaskStatus.Scheduled;
        if (t.StartTask.HasValue && !t.EndTask.HasValue)//we started work but didnt finish=in the middle
            return BO.TaskStatus.OnTrack;
        return BO.TaskStatus.Unscheduled;

    }
    private void InputIntegrityCheck(BO.Task item)
    {
        if (item.Id <= 0)
            throw new BO.BlNegtivePropertyException($"Engeineer's Id can not be negative");
        if (item.Name == "")
            throw new BO.BlNullPropertyException($"Engeineer's name can not be negative");
    }
    private DateTime? CalcMaxDate(DateTime? startTask, DateTime? ScheduleStart, TimeSpan? numDays)
       => startTask > ScheduleStart ? startTask + numDays : ScheduleStart + numDays;
}

