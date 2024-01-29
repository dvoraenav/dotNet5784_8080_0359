namespace DO;

public record Task
(
    int Id, //the id of the task
    string? Name, //the task's name
    string? Description, //the task's descripation
    string? Result, //a description of the result of the task
    DateTime? NewTask, //the time the task was created
    string? Comment = null, //comments on the task
    EngineerExpireance DifficultyLevel = 0, //the task's difficulty Level
    int EngneerId = 0,
    TimeSpan? NumDays=null, //the amount of days it takes to finish the task
    DateTime? ScheduleStart = null, //the task's schedule Starting time of the task
    DateTime? StartTask = null, //the task's starting time
    DateTime? Deadline = null, //the task's deadline
    DateTime? EndTask = null,   // the time the task was done
    int? EngineerId=null
)
{
    public Task() : this(0, "", "", "", null, "", 0,0, null, null, null, null,null,0) { }
}
