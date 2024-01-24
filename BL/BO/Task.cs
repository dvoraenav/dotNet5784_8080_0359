using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Task
    {
        public int Id { get; init; }
       
     public string? Name { get; set; } //the task's name
     public string? Description { get; set; } //the task's descripation
         public string? Result { get; set; } //a description of the result of the task
       public int NumDays { get; set; } //the amount of days it takes to finish the task
       public DateTime? NewTask { get; set; } //the time the task was created
       // bool? Mileston = null, //the task's milestone
   public string? Comment { get; set; } //comments on the task
       public int DifficultyLevel { get; init; } //the task's difficulty Level
       public DateTime? ScheduleStart { get; init; } //the task's schedule Starting time of the task
      public  DateTime? StartTask { get; init; } //the task's starting time
    public DateTime? Deadline { get; init; } //the task's deadline
        public DateTime? EndTask { get; init; }  // the time the task was done
    }
}
