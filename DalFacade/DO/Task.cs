namespace DO;

public record Task
(
    int id,
    string? name,
    string? description,
    bool mileston,
    DateTime? newTask,
    int numDays=0,
    string? result=null,
    string? comment = null,
    int engineerId = 0,
    int difficultyLevel = 0,
    DateTime? startTask=null,
    DateTime? scheduleStart = null,
    DateTime? deadline = null,
    DateTime? endTask= null
)
{
    public Task() : this(0, null, null, false,null) { }
}
