using System;

namespace Instagramme
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepo = new UserRepository();
            userRepo.GetXAmountOfUsers(10);

            Console.ReadLine();
        }
    }
}
