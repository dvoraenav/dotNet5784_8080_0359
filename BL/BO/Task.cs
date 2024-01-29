using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Task
{
    public int Id { get; init; }//the task id

    public string? Name { get; init; } //the task's name
    public string? Description { get; init; } //the task's descripation
    public string? Result { get; init; } //a description of the result of the task
    public int NumDays { get; init; } //the amount of days it takes to finish the task
    public DateTime? NewTask { get; init; } //the time the task was created
    public BO.TaskStatus Status { get; init; }
    public string? Comment { get; set; } //comments on the task
    public BO.EngineerExpireance DifficultyLevel { get; set; } //the task's difficulty Level
    public DateTime? ScheduleStart { get; set; } //the task's schedule Starting time of the task
    public DateTime? StartTask { get; set; } //the task's starting time
    public DateTime? Deadline { get; set; } //the task's deadline
    public DateTime? EndTask { get; set; }  // the time the task was done
    public BO.EngineerInTask? Engineer { get; set; }
    public List<BO.TaskInList>? TaskInList { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
