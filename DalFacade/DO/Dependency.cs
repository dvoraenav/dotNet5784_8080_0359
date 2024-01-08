

namespace DO;

public record Dependency
(   
    int id , //id of the dependncy
    int dependentTask , //the id of the current task
    int dependsLastTesk  //the id of the last task
)
{
 public Dependency() : this(0,0,0) { }
}