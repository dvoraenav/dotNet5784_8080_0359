

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
        string[] engineerNames =
        { "Dvora Eanv", "Riki Rubin", "Ranan Houri", "Shir Babayev", "David Tal", "Joe Biden" };
        foreach (var name in engineerNames)
        {
            int id;
            do
                id = s_rand.Next(2000000, 4000000);
            while (s_dalEngineer!.Read(id) != null);
            string mail = name+"@gmail";
            int pay = 35;
            Engineer newEngineer = new(id, name, mail, pay);
            s_dalEngineer!.Create(newEngineer);
        }
    }

    //private static void createDependency()
    //{
    //}
    private static void createTask()
    { 
        string[] TaskNames =
        { "Task1", "Task2", "Task3", "Task3", "Task4", "Task5" };
        foreach (var name in TaskNames) 
        { 
            int id=s_rand.Next(1, 100000);
            string des = "new";
            DateTime start = new DateTime(1955, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime newTask=start.AddDays(range);
            bool mil = false;
            int numd = 5;
            string result;
         }
    }
}