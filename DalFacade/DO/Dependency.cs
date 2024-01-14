

namespace DO;

public record Dependency
(   
    int Id , //id of the dependncy
    int CurrentTaskId , //the id of the current task
    int LastTaskId  //the id of the last task
)
{
 public Dependency() : this(0,0,0) { }
}