

namespace DO;

public record Dependency
(   
    int id = 0,
    int dependentTask = 0,
    int dependsOnTesk = 0
)
{
 public Dependency() : this(0) { }
}