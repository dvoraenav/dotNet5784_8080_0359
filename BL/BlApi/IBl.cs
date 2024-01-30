using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// 
/// </summary>

    public interface IBl
    {
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ITaskInEngineer taskInEngineer  { get; }
    public IEngineerInTask EngineerInTask { get; }
    public ITaskInList TaskInList { get; }

}

