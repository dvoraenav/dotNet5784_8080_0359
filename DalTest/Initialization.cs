namespace DalTest;
using DO;
using DalApi;
using System.Xml.Linq;

public static class Initialization
{
    private static IDal? s_dal;


    private static readonly Random s_rand = new();
    
    /// <summary>
    /// creating new engineers
    /// </summary>
    private static void createEngineer()
    {
        string[] fullNames =
        {"John Smith","Emma Johnson","Michael Williams","Sophia Jones","David Brown"};
        string[] emails = {
            "john.smith@email.com",
            "emma.johnson@email.com",
            "michael.williams@email.com",
            "sophia.jones@email.com",
            "david.brown@email.com"
        };
        for (int i = 0; i < fullNames.Length; i++)//going throw the arry of names
        {
            
            try {
                int id = s_rand.Next(200000000, 400000000);// a random number for id
                string name = fullNames[i];//taking the name [i] in the arry 
                string email = emails[i];//taking the email [i] in the arry 
                int pay = 35; //pay pre hour is set the same for all 5 engineers
                Engineer newEngineer = new(id, name, email, pay);//adding all the values to the object
                s_dal.Engineer!.Create(newEngineer);//trying to creat a new engineer 
            }
            catch (Exception e)//if there  is engineer with the radome id
            { i--; }//trying to cerat agine 
        }
    }
    /// <summary>
    /// creating new dependencies
    /// </summary>
    private static void createDependency()
    {
        int[,] dep = {
        {1, 2}, {2, 3}, {3, 4}, {4, 5},
        {5, 6}, {6, 7}, {7, 8}, {8, 9},
        {9, 10}, {10, 11}, {11, 12}, {12, 13},
        {13, 14}, {14, 15}, {15, 16}, {16, 17},
        {17, 18}, {18, 19}, {19, 20}, {20, 21},
        {21, 22}, {22, 23}, {23, 24}, {24, 25},
        {25, 26}, {26, 27}, {27, 28}, {28, 29},
        {29, 30}, {30, 31}, {31, 32}, {32, 33},
        {33, 34}, {34, 35}, {35, 36}, {36, 37},
        {37, 38}, {38, 39}, {39, 40} };
        //arry of dependency

       for (int i = 0;i < dep.GetLength(0);i++)
        { 
           int currentTID = dep[i, 1];//cureent dependency
           int oldTID = dep[i, 0]; //last dependency
           Dependency d= new Dependency(i, currentTID, oldTID);
            s_dal.Dependency!.Create(d);
        }
    
    }

    /// <summary>
    /// creatind new tasks
    /// </summary>
    private static void createTask()
    {
        string[] tasks =
        {
            "Define","Research","Scope","Plan","Stakeholders","Resources","Risk",
            "Milestones","Team","Communication","Design","Infrastructure","Coding",
            "Quality","Testing","Deployment","Monitor","Issues","Review","Closure"
        };//arry of tasks

        string[] taskDescriptions = {
            "Define project goals and objectives","Research and analyze construction requirements",
            "Scope the construction project","Develop a detailed construction plan",
            "Identify stakeholders in the construction project","Allocate construction resources and budget",
            "Assess and manage construction risks","Define construction milestones",
            "Assemble a construction project team","Develop a communication plan for construction",
            "Design the construction project architecture","Implement construction infrastructure",
            "Execute construction activities and coding","Ensure construction quality and compliance",
            "Conduct testing and inspection for construction","Deploy construction components",
            "Monitor construction progress","Address and resolve construction issues",
            "Conduct construction review and evaluation","Document and close the construction project"
        };//arry of description of the tasks
        for (int i = 0; i < tasks.Length; i++)
        {
            string name = tasks[i];//name of the task
            string description = taskDescriptions[i];//description of the task
            bool mile = false;//milston
            int numDays = 5;//nume days it will take to finish the task
            string result = "done";//result og the task
            string comment = "the task is completed";//task comment
            int dlevel = (i % 2) + 1;//difficulty level of the task
            int num = s_rand.Next(1, 30); //to create the date
            DateTime? newT = DateTime.Now.AddDays(num); //creation time
            Task newTask = new
               (Id:i,
                Name: name,
               Description: description,
               Mileston: mile,
               NumDays: numDays,
               Result: result,
               Comment: comment,
               DifficultyLevel: dlevel,
               NewTask: newT);
            s_dal.Task!.Create(newTask);
        }
    }
    public static void Do(IDal dal)
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");

        createDependency();
        createEngineer();
        createTask();
       

    }
}