

﻿using Dal;
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
                            EngineerManue();//manu of engineer
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                        case 2:
                        try
                        {
                            TaskManu();//manu of tasks
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                        case 3:
                        try
                        {
                            DependencyManu();//manu of dependency
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
                        Console.WriteLine("The program is ending");
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter engineer's valuses to add:");
                            Engineer eg = newEG(); //creating a new Enginerr
                            int id = s_dal.Engineer.Create(eg);
                            Console.WriteLine();//spaces
                            Console.WriteLine($"The engineer ={id} was created successfully");
                            Console.WriteLine();//spaces
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter engineer's ID");
                            int.TryParse(Console.ReadLine(), out Id);
                            Console.WriteLine(s_dal.Engineer.Read(x=>x.id==Id)); //printing engineer
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
                            int.TryParse(Console.ReadLine(), out Id);
                            s_dal.Engineer.Delete(Id);
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
                                int.TryParse(Console.ReadLine(), out Id);
                                Console.WriteLine(s_dal.Task.Read(x => x.id == Id));//printing task
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
                                int.TryParse(Console.ReadLine(), out Id);
                                s_dal.Task.Delete(Id);//deleting
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
                                int.TryParse(Console.ReadLine(), out Id);
                                Console.WriteLine(s_dal.Dependency.Read(x => x.id == Id));//printing dependency
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
                                int.TryParse(Console.ReadLine(), out Id);
                                s_dal.Dependency.Delete(Id);//deleting dependency
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
    public static Task newT()//creting a new Task
    {
        int Id = 0;
        Console.WriteLine("enter task's ID");
        int.TryParse(Console.ReadLine(), out Id);
        Console.WriteLine("enter task's name ");
        string? n = Console.ReadLine();//name
        Console.WriteLine("enter task's description");
        string? d = Console.ReadLine();//description
        Console.WriteLine("enter task's result ");
        string? r = Console.ReadLine();//result
        Console.WriteLine("enter comment");
        string? c = Console.ReadLine();//comment on the task
        int num = 0;//num days it will take to finish the task
        Console.WriteLine("enter num days it will take to do the project");
        int.TryParse(Console.ReadLine(), out num);
        Console.WriteLine("enter task's difficulty level ");
        int deLevel = 0;//level of diffculty
        int.TryParse(Console.ReadLine(), out deLevel);
        DateTime newT = DateTime.Now;
        //they rest of the object of dataTime will be initialized as NULL in the ctor
        Task newTask = new Task(Id,n,d,newT,false,num,r,c,deLevel);
        return newTask;
    }
    public static Dependency newDep()//creating a new dependecy
    {
        int Id = 0;
        Console.WriteLine("enter Dependency's ID");
        int.TryParse(Console.ReadLine(), out Id);
        int currTId=0;//id of the current task
        Console.WriteLine("enter current Task's ID");
        int.TryParse(Console.ReadLine(), out currTId);
        int oldTId=0;//if of the last task
        Console.WriteLine("enter last Task's ID");
        int.TryParse(Console.ReadLine(), out oldTId);
        Dependency dep = new Dependency(Id, currTId, oldTId);
        return dep;
    }
}

