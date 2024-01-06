

namespace DO;

public record Dependency
(   int id,
    int dependentTask,
    int dependsOnTesk
)
{
 public Dependency() : this(0) { }
public Dependency(int i, int dT, int dOT)
{ this.id = i;
        this.dependentTask = dT;
        this.dependsOnTesk = dOT;
}}