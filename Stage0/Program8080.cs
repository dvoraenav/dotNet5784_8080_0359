namespace Targil10
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8080();
            Welcome0359();
            Console.ReadKey();

        }

        private static void Welcome8080()
        {
            Console.Write("Enter your name: ");
            String username = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first application", username);
        }
        static partial void Welcome0359();
    }
}