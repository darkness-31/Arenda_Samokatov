using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arenda_Samokatov.Data
{
    internal class UsersExecution
    {
        public static ILiteCollection<Users> table = new List<Users>() {}.QueryCollection("Users");
        public static string Login = string.Empty;
        public static Permission Auntification()
        {
            Console.Title = "Авторизация";

            do
            {
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Until.NonVisibleReadLine();

                List<Users> list = new List<Users>();
                try
                {
                    list = table.Find(x => x.Password == password &&
                                           x.Login == login).ToList();

                    if (list.Count == 0)
                        throw new Exception("Неправильный логин и пароль");
                } 
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Console.ResetColor();
                    continue;
                }

                Login = login;
                return (Permission)list.First().Access;

            } while (true);
        }
    }
}
