using Dal;
using DalApi;
using DalTest;
using DO;
using System.Threading.Channels;
using Task = DO.Task;

internal class Program
{
    private static IEngineer s_dalEngineer = new EngineerImplementation();
    private static IDependency s_dalDapendncy = new DependencyImplementation();
    private static ITask s_dalTask = new TaskImplementation();
    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalEngineer, s_dalDapendncy, s_dalTask);
            int choice;
            Console.WriteLine(@"Hello!, 
                               please enter your choice
                               0: Exit Manu
                               1: Engineer
                               2: Task
                               3: Dependency");
            int.TryParse(Console.ReadLine(), out choice);
            do
            {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("the program is ending");
                        break;
                    case 1:
                        EngineerManue();
                        break;
                    case 2:
                        TaskManu();
                        break;
                    case 3:
                       DependencyManu();
                        break;
                }
            } while (choice != 0);
            
        }
        catch (Exception ex)
        { Console.WriteLine(ex); }
    }
    public static void EngineerManue()
    {
        try {
            int choice;
            Console.WriteLine(@"Hello!, 
                               please enter your choice
                               0: Exit Manu
                               1: Add Engineer
                               2: Print Engineer by Id
                               3: Print All Engineers
                               4: Update Engineer
                               5: Delete Engineer ");
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                int Id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("the program is ending");
                        break;
                    case 1:
                        Console.WriteLine("enter engineer's valuses to add:");
                        Engineer eg = newEG();
                        s_dalEngineer.Create(eg);
                        break;
                    case 2:
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine(s_dalEngineer.Read(Id));
                        break;
                    case 3:
                        Console.WriteLine(s_dalEngineer.ReadAll());
                        break;
                    case 4:
                        Console.WriteLine("enter engineer's valuses to update:");
                        Engineer e = newEG();
                        if (e != null)
                            s_dalEngineer.Update(e);
                        break;
                    case 5:
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        s_dalEngineer.Delete(Id);
                        break;
                }
             }
        }
        catch (Exception ex)
        { Console.WriteLine(ex); }
    }
    public static void TaskManu()
    {
        try
        {
            int choice;
            Console.WriteLine(@"Hello!, 
                               please enter your choice
                               0: Exit Manu
                               1: Add Task
                               2: Print Task by Id
                               3: Print All Tasks
                               4: Update Task
                               5: Delete Task ");
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                int Id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("the program is ending");
                        break;
                    case 1:
                        Task task = newT();
                        s_dalTask.Create(task);
                        break;
                    case 2:
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine(s_dalTask.Read(Id));
                        break;
                    case 3:
                        Console.WriteLine(s_dalTask.ReadAll());
                        break;
                    case 4:
                        Console.WriteLine("enter task's valuses to update:");
                        Task t = newT();
                        if (t != null)
                            s_dalTask.Update(t);
                        break;
                    case 5:
                        Console.WriteLine("enter task's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        s_dalTask.Delete(Id);
                        break;
                }
            }
        }
        catch (Exception ex)
        { Console.WriteLine(ex); }
    }
    public static void DependencyManu()
    {
        try
        {
            int choice;
            Console.WriteLine(@"Hello!, 
                               please enter your choice
                               0: Exit Manu
                               1: Add Dependency
                               2: Print Dependency by Id
                               3: Print All Dependency
                               4: Update Dependency
                               5: Delete Dependency ");
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                int Id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("the program is ending");//איך סוגרים תוכנ ית
                        break;
                    case 1:
                        Console.WriteLine("enter Dependency's valuses to update:");
                        Dependency dep = newDep();
                        s_dalDapendncy.Create(dep);
                        break;
                    case 2:
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine(s_dalDapendncy.Read(Id));
                        break;
                    case 3:
                        Console.WriteLine(s_dalDapendncy.ReadAll());
                        break;
                    case 4:
                        Console.WriteLine("enter Dependency's valuses to update:");
                        Dependency d = newDep();
                        if (d != null)
                            s_dalDapendncy.Update(d);
                        break;
                    case 5:
                        Console.WriteLine("enter Dependency's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        s_dalDapendncy.Delete(Id);
                        break;
                }
            }
        }
        catch (Exception ex)
        { Console.WriteLine(ex); }
    }
    public static void enumCheck(EngineerExpireance ex)
    {
        int choice;
        Console.WriteLine(@"Hello!, 
                               please enter engineer's level
                               0: Beginner
                               1: Advanced Beginner
                               2: Intermediate
                               3: Advanced
                               4: Expert");
        int.TryParse(Console.ReadLine(), out choice);
        
        switch (choice)
        {
            case 0:
                ex = EngineerExpireance.Beginner;
                break;
            case 1:
                ex = EngineerExpireance.Beginner;
                break;
            case 2:
                ex = EngineerExpireance.Beginner;
                break;
            case 3:
                ex = EngineerExpireance.Beginner;
                break;
            case 4:
                ex = EngineerExpireance.Beginner;
                break;
        }
    }
    public static Engineer newEG()
    {   
        int Id = 0;
        Console.WriteLine("enter engineer's ID");
        int.TryParse(Console.ReadLine(), out Id);
        Console.WriteLine("enter engineer's full name ");
        string? n = Console.ReadLine();
        Console.WriteLine("enter engineer's mail address ");
        string? m = Console.ReadLine();
        Console.WriteLine("enter engineer's cost per hour");
        int pay = 0;
        int.TryParse(Console.ReadLine(), out pay);
        int choice;
        Engineer eg;
        Console.WriteLine(@"Hello!, 
                               please enter engineer's level
                               0: Beginner
                               1: Advanced Beginner
                               2: Intermediate
                               3: Advanced
                               4: Expert");
        int.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 0:
                eg =new Engineer(Id, n, m, pay, EngineerExpireance.Beginner);
                break;
            case 1:
                eg = new Engineer(Id, n, m, pay, EngineerExpireance.AdvancedBeginner);
                break;
            case 2:
                eg = new Engineer(Id, n, m, pay, EngineerExpireance.Intermediate);
                break;
            case 3:
                eg = new Engineer(Id, n, m, pay, EngineerExpireance.Advanced);
                break;
            case 4:
                eg = new Engineer(Id, n, m, pay, EngineerExpireance.Expert);
                break;
            default: eg = new Engineer(Id, n, m, pay, EngineerExpireance.Beginner);
                break;
        }
        return eg;
    }
    public static Task newT()
    {
        int Id = 0;
        Console.WriteLine("enter task's ID");
        int.TryParse(Console.ReadLine(), out Id);
        Console.WriteLine("enter task's name ");
        string? n = Console.ReadLine();
        Console.WriteLine("enter task's description");
        string? d = Console.ReadLine();
        Console.WriteLine("enter task's result ");
        string? r = Console.ReadLine();
        Console.WriteLine("enter comment");
        string? c = Console.ReadLine();
        int num = 0;
        Console.WriteLine("enter num days it will take to do the project");
        int.TryParse(Console.ReadLine(), out num);
        Console.WriteLine("enter task's difficulty level ");
        int deLevel = 0;
        int.TryParse(Console.ReadLine(), out deLevel);
        DateTime newT = DateTime.Now;
        ///
        //int day, month, year;
        //Console.WriteLine("enter task's schdule time to start");
        //int.TryParse(Console.ReadLine(), out day);
        //int.TryParse(Console.ReadLine(), out month);
        //int.TryParse(Console.ReadLine(), out year);
        //DateTime sc = new DateTime(year, month, day);
        Task newTask = new Task(Id,n,d,newT,false,num,r,c,deLevel);//
        return newTask;
    }
    public static Dependency newDep()
    {
        int Id = 0;
        Console.WriteLine("enter Dependency's ID");
        int.TryParse(Console.ReadLine(), out Id);
        int TId=0;
        Console.WriteLine("enter current Task's ID");
        int.TryParse(Console.ReadLine(), out TId);
        int EId=0;
        Console.WriteLine("enter last Task's ID");
        int.TryParse(Console.ReadLine(), out EId);
        Dependency dep = new Dependency(Id,TId,EId);
        return dep;
    }
}
