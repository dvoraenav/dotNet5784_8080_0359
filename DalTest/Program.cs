using Dal;
using DalApi;
using DalTest;
using DO;
using System.Threading.Channels;
using System.Xml.Serialization;
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
                            EngineerManue();
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                        case 2:
                        try
                        {
                            TaskManu();
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                        case 3:
                        try
                        {
                            DependencyManu();
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
    public static int EngineerManue()
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
                int Id;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("the program is ending");
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("enter engineer's valuses to add:");
                            Engineer eg = newEG();
                            int id = s_dalEngineer.Create(eg);
                            Console.WriteLine();//spaces
                            Console.WriteLine(id);
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("enter engineer's ID");
                            int.TryParse(Console.ReadLine(), out Id);
                            Console.WriteLine(s_dalEngineer.Read(Id));
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine(s_dalEngineer.ReadAll());
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("enter engineer's valuses to update:");
                            Engineer e = newEG();
                            if (e != null)
                                s_dalEngineer.Update(e);
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("enter engineer's ID");
                            int.TryParse(Console.ReadLine(), out Id);
                            s_dalEngineer.Delete(Id);
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

    public static int TaskManu()
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
                while (choice != 0)
                {
                    int Id;
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("the program is ending");
                            break;
                        case 1:
                            try
                            {
                                Task task = newT();
                                s_dalTask.Create(task);
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 2:
                            try
                            {
                                Console.WriteLine("enter engineer's ID");
                                int.TryParse(Console.ReadLine(), out Id);
                                Console.WriteLine(s_dalTask.Read(Id));
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 3:
                            try
                            {
                                Console.WriteLine(s_dalTask.ReadAll());
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 4:
                            try
                            {
                                Console.WriteLine("enter task's valuses to update:");
                                Console.WriteLine();//spaces
                                Task t = newT();
                                if (t != null)
                                    s_dalTask.Update(t);
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 5:
                            try
                            {
                                Console.WriteLine("enter task's ID");
                                int.TryParse(Console.ReadLine(), out Id);
                                s_dalTask.Delete(Id);
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                    }
                }
            } while (choice != 0);
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
        return 0;
    }
    public static int DependencyManu()
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
                while (choice != 0)
                {
                    int Id;
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("the program is ending");
                            break;
                        case 1:
                            try
                            {
                                Console.WriteLine("enter Dependency's valuses to update:");
                                Dependency dep = newDep();
                                s_dalDapendncy.Create(dep);
                                Console.WriteLine();//spaces 
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 2:
                            try
                            {
                                Console.WriteLine("enter engineer's ID");
                                int.TryParse(Console.ReadLine(), out Id);
                                Console.WriteLine(s_dalDapendncy.Read(Id));
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 3:
                            try
                            {
                                Console.WriteLine(s_dalDapendncy.ReadAll());
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 4:
                            try
                            {
                                Console.WriteLine("enter Dependency's valuses to update:");
                                Dependency d = newDep();
                                if (d != null)
                                    s_dalDapendncy.Update(d);
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        case 5:
                            try
                            {
                                Console.WriteLine("enter Dependency's ID");
                                int.TryParse(Console.ReadLine(), out Id);
                                s_dalDapendncy.Delete(Id);
                                Console.WriteLine();//spaces
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                    }

                }
            } while (choice != 0);
           
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
        return 0;
    }
    
    public static Engineer newEG()// Getting values from the user buliding a new Engineer 
    {   
        int Id = 0;
        Console.WriteLine("enter engineer's ID");
        int.TryParse(Console.ReadLine(), out Id);
        Console.WriteLine("enter engineer's full name ");
        string? n = Console.ReadLine(); //name
        Console.WriteLine("enter engineer's mail address ");
        string? m = Console.ReadLine(); //mail
        Console.WriteLine("enter engineer's cost per hour");
        int pay = 0; //pay per hour
        int.TryParse(Console.ReadLine(), out pay);
        int choice;
        Engineer eg; // the new Engineer
        Console.WriteLine(@"Hello!, please enter your choice for level of expireance");
        Console.WriteLine(" 0: Beginner");
        Console.WriteLine(" 1: Advanced Beginner");
        Console.WriteLine(" 2: Intermediate");
        Console.WriteLine(" 3: Advanced");
        Console.WriteLine(" 4: Expert");
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
            default: 
                eg = new Engineer(Id, n, m, pay, EngineerExpireance.Beginner);
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
        //they rest of the object of dataTime will be initialized as NULL in the ctor
        Task newTask = new Task(Id,n,d,newT,false,num,r,c,deLevel);
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
