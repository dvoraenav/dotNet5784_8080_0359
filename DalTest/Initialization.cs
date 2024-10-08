﻿using DalApi;
using DO;

using System.Xml.Linq;
using Dal;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace DalTest;
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
        int[] tz =
            {273044483,215191420,243229927,217756230,251117987};
        for (int i = 0; i < fullNames.Length; i++)//going through the array of names
        {

            try
            { //s_rand.Next(200000000, 400000000)
                int id = tz[i];// a random number for id
                string name = fullNames[i];//taking the name [i] in the arry 
                string email = emails[i];//taking the email [i] in the arry 
                int pay = 35; //pay pre hour is set the same for all 5 engineers
                Engineer newEngineer = new(id, name, email, pay);//adding all the values to the object
                s_dal!.Engineer.Create(newEngineer);//trying to creat a new engineer 
            }
            catch (Exception ex) //if there  is engineer with the randome id
            {
                throw new Exception(ex.ToString());
                i--;
            }//trying to create again
        }
    }
    /// <summary>
    /// creating new dependencies
    /// </summary>
    private static void CreateDependency()
    {


        string[,] taskDependencies =
        {
        // CurrentTask, LastTask
        {"Research Needs", "Define Goals"},  // Research depends on Define
        {"Scope Project", "Define Goals"},  // Scope depends on Define
        {"Plan Strategy", "Scope Project"},  // Plan depends on Scope
        {"Allocate Resources", "Plan Strategy"},  // Resources depends on Plan
        {"Manage Risks", "Plan Strategy"},  // Risk depends on Plan
        {"Set Milestones", "Plan Strategy"},  // Milestones depends on Plan
        {"Build Team", "Allocate Resources"},  // Team depends on Resources
        {"Build Team", "Engage Stakeholders"},  // Team depends on Stakeholders
        {"Establish Communication", "Build Team"},  // Communication depends on Team
        {"Design Architecture", "Plan Strategy"},  // Design depends on Plan
        {"Implement Infrastructure", "Design Architecture"},  // Infrastructure depends on Design
        {"Code Functionality", "Implement Infrastructure"},  // Coding depends on Infrastructure
        {"Ensure Quality", "Code Functionality"},  // Quality depends on Coding
        {"Perform Testing", "Ensure Quality"},  // Testing depends on Quality
        {"Deploy Components", "Perform Testing"},  // Deployment depends on Testing
        {"Monitor Progress", "Deploy Components"},  // Monitor depends on Deployment
        {"Address Issues", "Monitor Progress"},  // Issues depends on Monitor
        {"Conduct Review", "Address Issues"},  // Review depends on Issues
        {"Document Closure", "Conduct Review"}  // Closure depends on Review
    };

        for (int i = 0; i < taskDependencies.GetLength(0); i++)
        {
            int currentTask = s_dal!.Task.Read(x=>x.Name== taskDependencies[i, 0])!.Id;
            int lastTask = s_dal.Task.Read(x => x.Name == taskDependencies[i, 1])!.Id;
            Dependency d = new Dependency( i,currentTask, lastTask);
            s_dal?.Dependency!.Create(d);
        }
    }


    /// <summary>
    /// creatind new tasks
    /// </summary>
    private static void CreateTask()
    {
        string[] tasks =
     {
        "Define Goals","Research Needs","Scope Project","Plan Strategy","Engage Stakeholders",
        "Allocate Resources","Manage Risks","Set Milestones","Build Team","Establish Communication",
        "Design Architecture","Implement Infrastructure","Code Functionality",
        "Ensure Quality","Perform Testing","Deploy Components","Monitor Progress",
        "Address Issues","Conduct Review","Document Closure"
    };

        string[] taskDescriptions =
        {
        "Define project goals", "Research construction requirements", "Scope the project",
        "Develop detailed project plan", "Identify project stakeholders",
        "Allocate project resources and budget", "Assess and manage project risks",
        "Define project milestones", "Assemble project team", "Develop communication plan",
        "Design project architecture", "Implement project infrastructure",
        "Code project functionalities", "Ensure project quality standards",
        "Conduct testing procedures", "Deploy project components", "Monitor project progress",
        "Address and resolve project issues", "Conduct project review", "Document project closure"
    };

        string[] taskResults =
        {
        "Clear project goals defined", "Construction requirements analyzed",
        "Project scope determined", "Detailed project plan created",
        "Stakeholders identified", "Resources allocated and budget set",
        "Risks assessed and managed", "Milestones defined",
        "Project team assembled", "Communication plan developed",
        "Architecture designed", "Infrastructure implemented",
        "Functionalities coded", "Quality standards ensured",
        "Testing conducted", "Components deployed",
        "Progress monitored", "Issues addressed",
        "Review conducted", "Closure documented"
    };//arry of description of the tasks
        for (int i = 0; i < tasks.Length; i++)
        {
            string name = tasks[i];//name of the task
            string description = taskDescriptions[i];//description of the task
            string result = taskResults[i];//result og the task
            string comment = "the task is wating to start";//task comment
            DateTime? newT = s_dal.Clock; //creation time
            TimeSpan? numDays = TimeSpan.FromDays(10 * (i + 1)); //creation time


            DO.Task newTask = new
               (
                Id: i,
                Name: name,
                Description: description,
                Result: result,
                Comment: comment,
                NewTask: newT,
                NumDays: numDays

               );
            if (s_dal is not null) s_dal.Task!.Create(newTask);
        }
    }
    public static void Do()
    {
        s_dal = DalApi.Factory.Get; 
        createEngineer();
        CreateTask();
        CreateDependency();
    }
    public static void Reset()
    {
      
        Factory.Get.Task.Clear();
        Factory.Get.Engineer.Clear();
        Factory.Get.Dependency.Clear();

    }
}
