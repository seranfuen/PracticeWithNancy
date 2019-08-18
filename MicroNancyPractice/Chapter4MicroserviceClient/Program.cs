using System;
using System.Threading.Tasks;

namespace Chapter4MicroserviceClient
{
    internal class Program
    {
        private static int _nextId = 1;

        private static void AddNewUser()
        {
            var client = new LoyaltyProgramClient();
            Console.Write("User name: ");
            var name = Console.ReadLine();
            var result = client.RegisterUser(new LoyaltyProgramUser {Id = _nextId++, Name = name}).Result;
            Console.WriteLine();
            if (result == null)
            {
                Console.WriteLine("Error!");
                return;
            }

            Console.WriteLine($"User registered with id {result.Id}");
        }

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(
                    "Type 'Exit' to exit. Type 'Query' to query user. Type 'NewUser' to add new user. Type 'UpdateUser' to update existing user");
                Console.WriteLine();

                var line = Console.ReadLine()?.Trim();
                if (string.Equals("exit", line, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (string.Equals("NewUser", line, StringComparison.OrdinalIgnoreCase))
                {
                    AddNewUser();
                }
                else if (string.Equals("UpdateUser", line, StringComparison.OrdinalIgnoreCase))
                {
                    UpdateUser();
                }
                else if (string.Equals("Query", line, StringComparison.OrdinalIgnoreCase))
                {
                    QueryUser();
                }
                else
                {
                    Console.WriteLine("Unknown command");
                    Console.WriteLine();
                }
            }
        }

        private static void QueryUser()
        {
            Console.Write("Type user id: ");
            var line = Console.ReadLine();
            if (int.TryParse(line, out var id))
            {
                Console.WriteLine();
                var client = new LoyaltyProgramClient();
                var user = client.QueryUser(id).Result;
                if (user == null)
                {
                    Console.WriteLine($"User {id} not found");
                }
                else
                {
                    Console.WriteLine($"Id: {id}");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Loyalty points: {user.LoyaltyPoints}");
                }
            }
            else
            {
                Console.WriteLine("Invalid id!");
            }
        }

        private static void UpdateUser()
        {
            var client = new LoyaltyProgramClient();
            Console.Write("Type user id: ");
            var line = Console.ReadLine();
            if (int.TryParse(line, out var id))
            {
                var user = client.QueryUser(id).Result;
                Console.Write("New user name:");
                user.Name = Console.ReadLine();
                var result = client.UpdateUser(user).Result;
                Console.WriteLine(result != null ? $"User {id} updated" : "Error updating!");
            }
            else
            {
                Console.WriteLine("Invalid id!");
            }
        }
    }
}