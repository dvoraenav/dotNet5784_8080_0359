namespace DO;

public record Task
(
    int Id, //the id of the task
    string? Name, //the task's name
    string? Description, //the task's descripation
    string? Result, //a description of the result of the task
    DateTime? NewTask, //the time the task was created
    bool? Mileston = null, //the task's milestone
    int? NumDays=null, //the anount of days it takes to finish the task
    string? Comment = null, //comments on the task
    int DifficultyLevel = 0, //the task's difficulty Level
    DateTime? ScheduleStart = null, //the task's schedule Starting time of the task
    DateTime? StartTask=null, //the task's starting time
    DateTime? Deadline = null, //the task's deadline
    DateTime? EndTask= null   // the time the task was done
)
{
    public Task() : this(0,null,null, null,null) { }
}
