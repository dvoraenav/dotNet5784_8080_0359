using Dal;
using DalApi;
using DalTest;
using DO;
using System.Threading.Channels;

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
                       // TaskManu();
                        break;
                    case 3:
                       // DependencyManu();
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
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine("enter engineer's full name ");
                        string n = Console.ReadLine();
                        Console.WriteLine("enter engineer's mail address ");
                        string m = Console.ReadLine();
                        Console.WriteLine("enter engineer's cost per hour");
                        int pay = 0;
                        int.TryParse(Console.ReadLine(), out pay);
                        //level
                        Engineer eg = new Engineer(Id, n, m, pay);
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
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine(s_dalEngineer.Read(Id));
                        Console.WriteLine("enter engineer's ID");
                        int.TryParse(Console.ReadLine(), out Id);
                        Console.WriteLine("enter engineer's full name ");
                        string na = Console.ReadLine();
                        Console.WriteLine("enter engineer's mail address ");
                        string ma = Console.ReadLine();
                        Console.WriteLine("enter engineer's cost per hour");
                        int p = 0;
                        int.TryParse(Console.ReadLine(), out pay);
                        //level
                        Engineer e = new Engineer(Id, na, ma, p);
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
}
