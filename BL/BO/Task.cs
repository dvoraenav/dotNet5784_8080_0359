using System.Xml.Serialization;

namespace BO;
public class Task
{
    public int Id { get; init; }//the task id
    public string? Name { get; init; } //the task's name
    public string? Description { get; init; } //the task's descripation
    public string? Result { get; init; } //a description of the result of the task
    public TimeSpan? NumDays { get; set; } //the amount of days it takes to finish the task
    public DateTime? NewTask { get; init; } //the time the task was created
    public TaskStatus Status { get; set; }
    public string? Comment { get; set; } //comments on the task
    public EngineerExpireance DifficultyLevel { get; set; } //the task's difficulty Level
    public DateTime? ScheduleStart { get; set; } //the task's schedule Starting time of the task
    public DateTime? StartTask { get; set; } //the task's starting time
    public DateTime? ForecastDate { get; set; } //the task's deadline
    public DateTime? EndTask { get; set; }  // the time the task was done
    public EngineerInTask? Engineer { get; set; }

    [XmlElement ("Depndencies") ]
    public List<TaskInList>? Depndencies { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
