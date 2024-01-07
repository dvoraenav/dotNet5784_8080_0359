
namespace DO;

public record Tesk
( int id,
  string? name,
  string? description,
  bool mileston,
  int numDays,
  string? result,
  string? comment,
  int engineerId,
  int difficultyLevel,
 DateTime? startTask,
  DateTime? newTask,
  DateTime? scheduleStart,
  DateTime? deadline,
  DateTime? endTask
)
{
    public Tesk() : this(0,null,null,false,0,null,null,0,0,null,null,null,null,null) { } 
}
