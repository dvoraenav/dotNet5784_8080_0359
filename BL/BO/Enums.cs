
namespace BO;
public enum EngineerExpireance //Expireance Level Of The Engineers
{
    Beginner = 0,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert,
    All
}
public enum TaskStatus
{
    Unscheduled,
    Scheduled,
    OnTrack,
    Done,
    All
}
public enum EnginnerFilter
{
    None,
    FilterByExpireance,
    FilterByTask
}
public enum TaskFilter
{
    None,
    FilterByExpireance,
    FilterByEngineer,
    FilterByAvailability
}
