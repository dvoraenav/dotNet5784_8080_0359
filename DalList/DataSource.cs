
namespace Dal;
internal static class DataSource
{
    internal static class Config
    {
        //auto id numbers for dependency
        internal const int dependencyId = 1;
        private static int nextDependencyId = dependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
        //auto id numbers for task
        internal const int taskID = 1;
        private static int nextTaskID = taskID;
        internal static int NextTaskID { get => nextTaskID++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();//list of engineers
    internal static List<DO.Dependency> Dependencies { get; } = new();//list of dependency
    internal static List<DO.Task> Tasks { get; } = new();//list of task

}
