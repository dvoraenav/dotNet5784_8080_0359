﻿
namespace Dal;
internal  static class DataSource
{
    internal static class Config
    {
        //atuo id numbers for dependency
        internal const int dependencyId = 0;
        private static int nextDependencyId = dependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
        //atuo id numbers for task
        internal const int taskID = 0;
        private static int nextTaskID = taskID;
        internal static int NextTaskID { get => nextTaskID++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();

}
