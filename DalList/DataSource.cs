
namespace Dal;
internal  static class DataSource
{
    internal static class Config
    {
        internal const int dependencyId = 1;
        internal const int taskID = 1;
        private static int nextDependencyId = dependencyId;
        private static int nextTaskID = taskID;
        internal static int NextDependencyId { get => nextDependencyId++; }
        internal static int NextTaskID { get => nextTaskID++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tesks { get; } = new();

}
