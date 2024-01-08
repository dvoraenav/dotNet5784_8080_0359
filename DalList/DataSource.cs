
namespace Dal;
internal  static class DataSource
{
    internal static class Config
    {
        internal const int dependencyId = 0;
        internal const int taskID = 0;
        private static int nextDependencyId = dependencyId;
        private static int nextTaskID = taskID;
        internal static int NextDependencyId { get => nextDependencyId++; }
        internal static int NextTaskID { get => nextTaskID++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();

}
