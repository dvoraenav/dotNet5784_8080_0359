using DalApi;
using DalTest;
namespace DalTest
{
    internal class Program
    {
        private static IEngineer? eg = new EngineerImlementation(); //stage 1
        private static ITask? t1 = new TaskImlementation(); //stage 1
        private static IDependency? d1 = new DependencyImlementation(); //stage 1
        public static void Main(string[] args)
        {
            Initialization.Do(eg, t1, d1);
            string


        }
    }
}