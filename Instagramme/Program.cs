using System;

namespace Instagramme
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepo = new UserRepository();

            //Console.WriteLine("Please enter your name:");
            //string name = Console.ReadLine();
            //userRepo.AddUser(name);

            Console.WriteLine("How many users would you like to see?");
            string number = Console.ReadLine();
            userRepo.GetXAmountOfUsers(number);

            Console.ReadLine();
        }
    }
}
