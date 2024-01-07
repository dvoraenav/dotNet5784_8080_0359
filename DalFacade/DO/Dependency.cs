

namespace DO;

public record Dependency
(   int id,
    int dependentTask,
    int dependsOnTesk
)
{
 public Dependency() : this(0,0,0) { }
}