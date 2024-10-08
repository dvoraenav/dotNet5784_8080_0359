﻿namespace BO;

public class TaskInGantt
{
    public int Id { get; set; } 
    public string Name { get; set; }    
    public int StartOffset { get; set; }
    public int TaskLenght { get; set; } 
    public TaskStatus Status { get; set; }  
    public int CompliteValue { get; set; }
    public List<string>? DepndenciesNames { get; set; }
}
