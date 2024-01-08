namespace DO;

public record Task
(
    int id, //the id of the task
    string? name, //the task's name
    string? description, //the task's descripation
    DateTime? newTask, //the time the task was created
    bool? mileston = null, //the task's milestone
    int? numDays=null, //the anount of days it takes to finish the task
    string? result=null, //a description of the result of the task
    string? comment = null, //comments on the task
    int difficultyLevel = 0, //the task's difficulty Level
    DateTime? scheduleStart = null, //the task's schedule Starting time of the task
    DateTime? startTask=null, //the task's starting time
    DateTime? deadline = null, //the task's deadline
    DateTime? endTask= null   // the time the task was done
)
{
    public Task() : this(0, null, null,null) { }
}
