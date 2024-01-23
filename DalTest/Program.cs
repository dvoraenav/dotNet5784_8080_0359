

using Dal;
using DalApi;
using DalTest;
using DO;
using Task = DO.Task;

internal class Program
{
    //static readonly IDal s_dal = new DalList();
    static readonly IDal s_dal = new DalXml(); //stage 3


    private static void Main(string[] args)
    {
        try
        {

            int choice;
            do
            {

                Console.WriteLine(@"Hello!, please enter your choice");
                Console.WriteLine(" 0: Exit Manu");
                Console.WriteLine(" 1: Engineer");
                Console.WriteLine(" 2: Task");
                Console.WriteLine(" 3: Dependency");
                Console.WriteLine(" 4: Initial Data");
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
                    case 4:
                        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                        if (ans == "Y") //stage 3
                        {
                            s_dal.Task.Clear();
                            s_dal.Dependency.Clear();
                            s_dal.Engineer.Clear();
                            Initialization.Do(s_dal);
                        } //stage 2
                        break;

                }

            } while (choice != 0);

        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }
    }
    /// <summary>
    /// menu of engineer entity
    /// </summary>
    /// <returns>exsit the entity menu</returns>
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
                        Console.WriteLine("Exsiting the menu");
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
                            Console.Write("Enter engineer's id to update: ");
                            int.TryParse(Console.ReadLine(), out int iD);
                            Console.WriteLine("Current engineer values");
                            Console.WriteLine(s_dal.Engineer.Read(x => x.Id == iD)); //printing engineer
                            Console.WriteLine();//spaces
                            Console.WriteLine("If you do not want to update a certain field press ENTER");
                            Engineer e = updateEG(s_dal.Engineer.Read(x => x.Id == iD)!);
                            if (e != s_dal.Engineer.Read(x => x.Id == iD))//if there are updates in the object
                                s_dal.Engineer.Update(e); //updating values on engineer
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

    /// <summary>
    /// Menu of task entity
    /// </summary>
    /// <returns> exsiting the entity menu</returns>
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
                        Console.WriteLine("Exsiting the menu");
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
                            Console.Write("Enter task's id to update: ");
                            int.TryParse(Console.ReadLine(), out int iD);
                            Console.WriteLine("Current task values");
                            Console.WriteLine(s_dal.Task.Read(x => x.Id == iD)); //printing task
                            Console.WriteLine();//spaces
                            Console.WriteLine("If you do not want to update a certain field press ENTER");
                            Task t = updateT(s_dal.Task.Read(x => x.Id == iD)!);
                            if (t != s_dal.Task.Read(x => x.Id == iD))//if we updated the task
                            {
                                Task newT = t with { NewTask = DateTime.Now }; //setting a new creation time
                                s_dal.Task.Update(t); //updating values 
                            }
                            Console.WriteLine();//spaces
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
    /// <summary>
    /// menu of dependency entity
    /// </summary>
    /// <returns>exsiting the menu</returns>
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
                        Console.WriteLine("Exsiting the menu");
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
                            Console.Write("Enter dependency's id to update: ");
                            int.TryParse(Console.ReadLine(), out int iD);
                            Console.WriteLine("Current dependency values");
                            Console.WriteLine(s_dal.Task.Read(x => x.Id == iD)); //printing dependency
                            Console.WriteLine();//spaces
                            Console.WriteLine("If you do not want to update a certain field press ENTER");
                            Dependency dep = updateDep(s_dal.Dependency.Read(x => x.Id == iD)!);
                            if (dep != s_dal.Dependency.Read(x => x.Id == iD))//if we updated the object
                                s_dal.Dependency.Update(dep); //updating values og engineer
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

    /// <summary>
    /// Getting values  from the user ​​and creating a new object
    /// </summary>
    /// <returns>new object</returns>
    public static Engineer newEG()
    {
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine(), out int id);//id
        Console.WriteLine("Enter engineer's full name ");
        string? name = Console.ReadLine(); //name
        Console.WriteLine("Enter engineer's mail address ");
        string? mail = Console.ReadLine(); //mail
        Console.WriteLine("Enter engineer's cost per hour");
        double payPerHour = 0; //pay per hour
        double.TryParse(Console.ReadLine(), out payPerHour);
        int choice;
        Console.WriteLine(@"Please enter your choice for level of expireance");
        Console.WriteLine(" 1: Beginner");
        Console.WriteLine(" 2: Advanced Beginner");
        Console.WriteLine(" 3: Intermediate");
        Console.WriteLine(" 4: Advanced");
        Console.WriteLine(" 5: Expert");
        EngineerExpireance expireance = new EngineerExpireance();
        int.TryParse(Console.ReadLine(), out choice); //level of expireance
        switch (choice)
        {
            case 1:
                expireance = EngineerExpireance.Beginner;
                break;
            case 2:
                expireance = EngineerExpireance.AdvancedBeginner;
                break;
            case 3:
                expireance = EngineerExpireance.Intermediate;
                break;
            case 4:
                expireance = EngineerExpireance.Advanced;
                break;
            case 5:
                expireance = EngineerExpireance.Expert;
                break;
            case 0:
                expireance = EngineerExpireance.Beginner;
                break;
        }
        Engineer eg = new(Id: id, FullName: name!, Mail: mail!, PayPerHour: payPerHour, Level: expireance);
        return eg;
    }

    /// <summary>
    /// Getting values from the user ​​and creating a new object
    /// </summary>
    /// <returns>new object</returns>
    public static Task newT()
    {
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine(), out int id);//id
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
        //the rest of the object of dataTime will be initialized as NULL in the ctor
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

    /// <summary>
    /// Getting values from the user ​​and creating a new object
    /// </summary>
    /// <returns>new object</returns>
    public static Dependency newDep()
    {
        Console.WriteLine("Enter dependency's ID");
        int.TryParse(Console.ReadLine(), out int id);//id
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

    /// <summary>
    /// Getting new values from the user and creating a new object
    /// </summary>
    /// <param name="engineer"> the current objects values</param>
    /// <returns> new object </returns>
    public static Engineer updateEG(Engineer engineer)
    {
        Console.WriteLine("Enter new id");
        int.TryParse(Console.ReadLine(), out int id);
        if (id == 0) id = engineer.Id; // if the input is empty use the old value
        Console.WriteLine("Enter new full name:");
        string? name = Console.ReadLine(); //name
        if (name == "") name = engineer.FullName; // if the input is empty use the old value
        Console.WriteLine("Enter new mail address ");
        string? mail = Console.ReadLine(); //mail
        if (mail == "") mail = engineer.Mail; // if the input is empty use the old value
        Console.WriteLine("Enter new cost per hour");
        double payPerHour = 0;
        double.TryParse(Console.ReadLine(), out payPerHour);
        if (payPerHour == 0) payPerHour = engineer.PayPerHour; // if the input is empty use the old value
        int choice;
        Console.WriteLine(@"Please enter your new choice for level of expireance");
        Console.WriteLine(" 1: Beginner");
        Console.WriteLine(" 2: Advanced Beginner");
        Console.WriteLine(" 3: Intermediate");
        Console.WriteLine(" 4: Advanced");
        Console.WriteLine(" 5: Expert");
        EngineerExpireance expireance = new EngineerExpireance();
        int.TryParse(Console.ReadLine(), out choice); //level of expireance
        switch (choice)
        {
            case 1:
                expireance = EngineerExpireance.Beginner;
                break;
            case 2:
                expireance = EngineerExpireance.AdvancedBeginner;
                break;
            case 3:
                expireance = EngineerExpireance.Intermediate;
                break;
            case 4:
                expireance = EngineerExpireance.Advanced;
                break;
            case 5:
                expireance = EngineerExpireance.Expert;
                break;
            case 0:
                expireance = engineer.Level; // if the input is empty use the old value
                break;
        }
        Engineer eg = new(Id: id, FullName: name!, Mail: mail!, PayPerHour: payPerHour, Level: expireance);

        return eg;//the update
    }

    /// <summary>
    /// Getting new values from the user and creating a new object
    /// </summary>
    /// <param name="task"> the current objects values </param>
    /// <returns> the new object </returns>
    public static Task updateT(Task task)
    {
        int id = task.Id;
        Console.WriteLine("Enter new name ");
        string? name = Console.ReadLine();//name
        if (name == "") name = task.Name; // if the input is empty use the old value
        Console.WriteLine("Enter task's description");
        string? description = Console.ReadLine();//description
        if (description == "") description = task.Description; // if the input is empty use the old value
        Console.WriteLine("Enter task's result ");
        string? result = Console.ReadLine();//result
        if (result == "") result = task.Result; // if the input is empty use the old value
        Console.WriteLine("Enter num days it will take to do the project");
        int.TryParse(Console.ReadLine(), out int numDays);//num days it will take to finish the task
        if (numDays == 0) numDays = (int)task.NumDays; // if the input is empty use the old value
        Console.WriteLine("Enter task's difficulty level ");
        int.TryParse(Console.ReadLine(), out int deLevel);
        if (deLevel == 0) deLevel = task.DifficultyLevel;// if the input is empty use the old value
        DateTime newT = (DateTime)task.NewTask!;
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

    /// <summary>
    /// Getting new values from the user and creating a new object
    /// </summary>
    /// <param name="dependency"> the current objects values</param>
    /// <returns> new object</returns>
    public static Dependency updateDep(Dependency dependency)
    {
        int id = dependency.Id;
        Console.WriteLine("Enter current task's ID");
        int.TryParse(Console.ReadLine(), out int currTId);//id of the current task
        if (currTId == 0) currTId = (int)dependency.CurrentTaskId;// if the input is empty use the old value
        Console.WriteLine("Enter last task's ID");
        int.TryParse(Console.ReadLine(), out int lastTId);//id of the last task
        if (lastTId == 0) lastTId = (int)dependency.LastTaskId;// if the input is empty use the old value
        Dependency dep = new(
            Id: id,
            CurrentTaskId: currTId,
            LastTaskId: lastTId);
        return dep;
    }
}

