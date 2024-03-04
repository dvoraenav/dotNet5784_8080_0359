using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BlApi;
using BO;
using Task = BO.Task;

namespace BlTest;

//I used XML files here.
//Each time a change is made in the test file and a different test is performed
internal class Program
{
    static readonly IBl bl = BlApi.Factory.Get;
    static readonly DalApi.IDal dal = DalApi.Factory.Get;
    static readonly string TaskPath = $@"..\\xml\\TaskTest.xml";
    static readonly string EngPath = $@"..\\xml\\EngineerTest.xml";


    static void Main()
    {

        while (true)
        {
            Console.WriteLine(
                "to engineer action press 1\n" +
                "to taks action press 2\n" +
                "to intialize the data press 3 \n" +
                "EXIT press 4");

            int action = int.Parse(Console.ReadLine()!);

            switch (action)
            {
                case 1:
                    EngineerMain();
                    break;

                case 2:
                    TaskMain();
                    break;

                case 3:
                    Console.Write("Would you like to create Initial data? (Y/N)\n");
                    string ans = Console.ReadLine() ?? "Y";
                    //if (ans == "Y")
                    //    Initialization.Do();
                    break;

                case 4:
                    return;

                default:
                    break;
            }
        }
    }

    static void EngineerMain()
    {

        int Id;
        while (true)
        {
            Task engineer = new Task(); //{ Id = 15, FullName = "bhsdfs", Level = EngineerExpireance.Intermediate, Mail = "kfj", PayPerHour = 2.2, Task = new() };
            TaskInEngineer task = new TaskInEngineer();
            Console.WriteLine(
                "Add engineer press 1\n" +
                "Read engineer press 2\n" +
                "Update engineer press 3\n" +
                "Read all engineers press 4\n" +
                "Delete engineer press 5\n" +
                "Back to main press 6\n");

            int action = int.Parse(Console.ReadLine()!);
            try
            {
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Enter the data to test xml and press Y");
                        string accept = Console.ReadLine()!;
                        //Save(engineer);
                        if (accept == "Y")
                        {
                            bl.Engineer.Create(LoadTest<Engineer>(EngPath));
                        }
                        break;

                    case 2:
                        Console.Write("Enter Engineer Id: ");
                        Id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine((object)bl.Engineer!.Read(Id));
                        break;

                    case 3:
                        Console.WriteLine("Enter the data to test xml and press Y");
                        accept = Console.ReadLine()!;
                        if (accept == "Y")
                            bl.Engineer.Update(LoadTest<Engineer>(EngPath));
                        break;

                    case 4:
                        foreach (var eng in bl.Engineer.GetEngineerList())
                            Console.WriteLine(eng);
                        break;
                    case 5:
                        Id = int.Parse(Console.ReadLine()!);
                        bl.Engineer.Delete(Id);
                        break;
                    case 6:
                        return;
                    default:
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

    }

    static void TaskMain()
    {
        int id;
        while (true)
        {
            Task task = new Task() { };
            Console.WriteLine(
                "Add task press 1\n" +
                "Read task press 2\n" +
                "Read all engineers press 3\n" +
                "Update task press 4\n" +
                "Delete task press 5\n" +
                "Update date press 6\n" +
                "Back to main press 7\n");

            int action = int.Parse(Console.ReadLine()!);
            try
            {
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Enter the data to test xml and press Y");
                        string accept = Console.ReadLine()!;
                        //task = bl.Task.ReadTask(1)!;
                        //Save(task);
                        if (accept == "Y")
                            bl.Task.Create(LoadTest<Task>(TaskPath));
                        break;

                    case 2:
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(bl.Task.ReadTask(id));
                        break;

                    case 3:
                        foreach (var t in bl.Task.TaskList())
                            Console.WriteLine(t);
                        break;

                    case 4:
                        Console.WriteLine("Enter the data to test xml and press Y");
                        accept = Console.ReadLine()!;
                        if (accept == "Y")
                            bl.Task.Update(LoadTest<Task>(TaskPath));
                        break;
                    case 5:
                        Console.WriteLine("Enter delete task id\r\n");
                        id = int.Parse(Console.ReadLine()!);
                        bl.Task.Delete(id);
                        break;

                    case 6:
                        DateTime time = DateTime.MinValue;
                        id = int.Parse(Console.ReadLine()!);
                        bl.Task.UpdateStartingDate(id, time);
                        break;

                    case 7:
                        return;

                    default:
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

    }

    private static void Save(Task e)
    {
        using FileStream file = new(TaskPath, FileMode.Create, FileAccess.Write, FileShare.None);
        new XmlSerializer(typeof(Task)).Serialize(file, e);
    }

    private static Item LoadTest<Item>(string filePath) where Item : class, new()
    {
        using FileStream file = new(filePath, FileMode.Open);
        XmlSerializer x = new(typeof(Item));
        return x.Deserialize(file) as Item ?? new();
    }

}
