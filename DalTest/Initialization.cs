

namespace DalTest;
using DO;
using DalApi;

public static class Initialization
{

    private static IEngineer? s_dalEngineer;// stage1
    private static IDependency? s_dalDependency;//stage 1
    private static ITask? s_dalTask;//stage 1

    private static readonly Random s_rand = new();
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
        for (int i = 0; i < fullNames.Length; i++)
        {
            int id;
            do
                id = s_rand.Next(2000000, 4000000);
            while (s_dalEngineer!.Read(id) != null);
            string name = fullNames[i];
            string email = emails[i];
            int pay = 35;
            Engineer newEngineer = new(id, name, email, pay);
            s_dalEngineer!.Create(newEngineer);
        }
    }

    private static void createDependency()
    {//לבדוקק
        int[,] dep = { {1,1},{ 1, 2 },{ 1,3},{ 1, 4},
                       {2,1},{ 2, 2 },{ 2,5},{ 2, 4},
                       {3,1},{ 3, 3 },{ 3,5},{ 3, 4},
                       {4,1},{ 4, 2 },{ 5,3},{ 4, 3},
                       {5,1},{ 5, 2 },{4, 5}, {5, 4},};
        foreach (int i in dep)
        { 
           int eID = dep[i, 0];
           int dID = dep[0, i];
           Dependency d= new Dependency(i,eID, dID);
           s_dalDependency!.Create(d);
        }
    
    }
    private static void createTask()
    {
        string[] tasks =
        {
            "Define","Research","Scope","Plan","Stakeholders","Resources","Risk",
            "Milestones","Team","Communication","Design","Infrastructure","Coding",
            "Quality","Testing","Deployment","Monitor","Issues","Review","Closure"
        };

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
        };
        for (int i = 0; i < tasks.Length; i++)
        {
            string name = tasks[i];
            string des = taskDescriptions[i];
            bool mile = false;
            int numD = 5;
            string result = "done";
            string comment = "the task is compilted";
            int dlevel = (i % 2) + 1;
            int num = s_rand.Next(1, 30);
            DateTime? newT = DateTime.Now.AddDays(num);
            DateTime? startT = DateTime.Now.AddDays(num + 1);
            DateTime? schhdual = DateTime.Now.AddDays(num + 5);
            DateTime? deadline = DateTime.Now.AddDays(num + 10);
            DateTime? end = DateTime.Now.AddDays(num + 7);
            Task newTask = new Task(num, name, des, newT, mile, numD, result, comment, dlevel, schhdual, startT, deadline, end);
            s_dalTask!.Create(newTask);
        }
    }
    public static void Do(IEngineer dalEngineer,IDependency dalDependency,ITask dalTask)
    {
        dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
        createEngineer();
        createTask();
    }
}