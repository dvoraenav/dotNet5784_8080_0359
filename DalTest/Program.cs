

using Dal;
using DalApi;
using DalTest;
using DO;
using System.Threading.Channels;
using System.Xml.Serialization;
using Task = DO.Task;

internal class Program
{
    //private static IEngineer s_dalEngineer = new EngineerImplementation();
    //private static IDependency s_dalDapendncy = new DependencyImplementation();
    //private static ITask s_dalTask= new TaskImplementation();
    static readonly IDal s_dal = new DalList(); //stage 2

    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dal);
            int choice;
            do
            {
                Console.WriteLine(@"Hello!, please enter your choice");
                Console.WriteLine(" 0: Exit Manu");
                Console.WriteLine(" 1: Engineer");
                Console.WriteLine(" 2: Task");
                Console.WriteLine(" 3: Dependency");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Bye");
                        break;
                    case 1:
                        try
                        {
                            EngineerMenu();//manu of engineer
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            TaskMenu();//manu of tasks
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try
                        {
                            DependencyMenu();//manu of dependency
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                }

            } while (choice != 0);

        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
    }
    public static int EngineerMenu()
    {
        try
        {
            int choice;
            do
            {
                Console.WriteLine(@"Hello!, please enter your choice");
                Console.WriteLine(" 0: Exit Manu");
                Console.WriteLine(" 1: Add Engineer");
                Console.WriteLine(" 2: Print Engineer by Id");
                Console.WriteLine(" 3: Print All Engineers");
                Console.WriteLine(" 4: Update Engineer");
                Console.WriteLine(" 5: Delete Engineer");
                int.TryParse(Console.ReadLine(), out choice);
                int id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("The program is ending");
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter engineer's valuses to add:");
                            Engineer eg = newEG(); //creating a new Enginerr
                            int newId = s_dal.Engineer.Create(eg);
                            Console.WriteLine();//spaces
                            Console.WriteLine($"The engineer {newId} was created successfully");
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter engineer's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(s_dal.Engineer.Read(x => x.Id == id)); //printing engineer
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try
                        {
                            foreach (var engineer in s_dal.Engineer.ReadAll())
                                Console.WriteLine(engineer); //printing all engineers
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Enter engineer's valuses to update:");
                            Console.WriteLine();//spaces
                            Engineer e = newEG();
                            if (e != null)
                                s_dal.Engineer.Update(e); //updating values og engineer
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("Enter engineer's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            s_dal.Engineer.Delete(id);
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                }
            } while (choice != 0);
        }

        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
        return 0;
    }

    public static int TaskMenu()
    {
        try
        {
            int choice;
            do
            {
                Console.WriteLine(@"Hello!, please enter your choice");
                Console.WriteLine(" 0: Exit Manu");
                Console.WriteLine(" 1: Add Task");
                Console.WriteLine(" 2: Print Task by Id");
                Console.WriteLine(" 3: Print All Task");
                Console.WriteLine(" 4: Update Task");
                Console.WriteLine(" 5: Delete Task");
                int.TryParse(Console.ReadLine(), out choice);
                int id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("The program is ending");
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter values for a new task");
                            Task task = newT();//creating a new task
                            s_dal.Task.Create(task);//adding a new task
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter task's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(s_dal.Task.Read(x => x.Id == id));//printing task
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try
                        {
                            foreach (var task in s_dal.Task.ReadAll())
                            {
                                Console.WriteLine(task); //printing all tasks
                                Console.WriteLine();//spaces 
                            }
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Enter values to update task");
                            Console.WriteLine();//spaces
                            Task t = newT();
                            if (t != null)
                                s_dal.Task.Update(t);//apdating task values
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("Enter task's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            s_dal.Task.Delete(id);//deleting
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                }
            } while (choice != 0);
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
        return 0;
    }
    public static int DependencyMenu()
    {
        try
        {
            int choice;
            do
            {
                Console.WriteLine(@"Hello!, please enter your choice");
                Console.WriteLine(" 0: Exit Manu");
                Console.WriteLine(" 1: Add Dependency");
                Console.WriteLine(" 2: Print Dependency by Id");
                Console.WriteLine(" 3: Print All Dependencys");
                Console.WriteLine(" 4: Update Dependency");
                Console.WriteLine(" 5: Delete Dependency");
                int.TryParse(Console.ReadLine(), out choice);
                int id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("The program is ending");
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Dependency's valuses to update:");
                            Dependency dep = newDep();//creating a new dependency
                            s_dal.Dependency.Create(dep);//adding a new dependency
                            Console.WriteLine();//spaces 
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter dependency's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(s_dal.Dependency.Read(x => x.Id == id));//printing dependency
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try
                        {
                            foreach (var dep in s_dal.Dependency.ReadAll())
                                Console.WriteLine(dep); //printing all dependencies
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Enter Dependency's valuses to update:");
                            Dependency d = newDep();
                            if (d != null)
                                s_dal.Dependency.Update(d); //updating dependency
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("enter Dependency's ID");
                            int.TryParse(Console.ReadLine(), out id);
                            s_dal.Dependency.Delete(id);//deleting dependency
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                }

            
        } while (choice != 0);

        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
        return 0;
    }

    public static Engineer newEG()// Getting values from the user buliding a new Engineer 
    {
        Console.WriteLine("enter engineer's ID");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("enter engineer's full name ");
        string? name = Console.ReadLine(); //name
        Console.WriteLine("enter engineer's mail address ");
        string? mail = Console.ReadLine(); //mail
        Console.WriteLine("enter engineer's cost per hour");
        int payPerHour = 0; //pay per hour
        int.TryParse(Console.ReadLine(), out payPerHour);
        int choice;
        Engineer eg;
        Console.WriteLine(@"Please enter your choice for level of expireance");
        Console.WriteLine(" 0: Beginner");
        Console.WriteLine(" 1: Advanced Beginner");
        Console.WriteLine(" 2: Intermediate");
        Console.WriteLine(" 3: Advanced");
        Console.WriteLine(" 4: Expert");
        int.TryParse(Console.ReadLine(), out choice); //level of expireance
        switch (choice)
        {
            case 0:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.Beginner);
                break;
            case 1:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.AdvancedBeginner);
                break;
            case 2:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.Intermediate);
                break;
            case 3:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.Advanced);
                break;
            case 4:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.Expert);
                break;
            default:
                eg = new(Id: id, FullName: name, Mail: mail, PayPerHour: payPerHour, Level: EngineerExpireance.Beginner);
                break;
        }
        return eg;
    }
    public static Task newT()//creting a new Task
    {
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("Enter task's name ");
        string? name = Console.ReadLine();//name
        Console.WriteLine("Enter task's description");
        string? description = Console.ReadLine();//description
        Console.WriteLine("Enter task's result ");
        string? result = Console.ReadLine();//result
        Console.WriteLine("Enter num days it will take to do the project");
        int.TryParse(Console.ReadLine(), out int numDays);//num days it will take to finish the task
        Console.WriteLine("Enter task's difficulty level ");
        int deLevel = 0;//level of diffculty
        int.TryParse(Console.ReadLine(), out deLevel);
        DateTime newT = DateTime.Now;
        //they rest of the object of dataTime will be initialized as NULL in the ctor
        Task newTask = new(
            Id: id,
            Name: name,
            Description: description,
            NewTask: newT,
            NumDays: numDays,
            Result: result,
            DifficultyLevel: deLevel);
        return newTask;
    }
    public static Dependency newDep()//creating a new dependecy
    {
        Console.WriteLine("Enter dependency's ID");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("Enter current task's ID");
        int.TryParse(Console.ReadLine(), out int currTId);//id of the current task
        Console.WriteLine("Enter last task's ID");
        int.TryParse(Console.ReadLine(), out int lastTId);//id of the last task
        Dependency dep = new(
            Id: id,
            CurrentTaskId: currTId,
            LastTaskId: lastTId);
        return dep;
    }
}

