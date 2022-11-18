using Microsoft.Data.Sqlite;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Arenda_Samokatov;

internal class Program
{
    private static string Login = string.Empty;
    private static bool SuperUser = false;

    public static void Main(string[] args)
    {
        switch (Authorization())
        {
            case Permission.Admin:
                Console.Title = $"Прокат самокатов - {Login} (Администратор)";
                SuperUser = true;
                break;
            case Permission.User:
                Console.Title = $"Прокат самокатов - {Login} (Пользователь)";
                break;
        }

        bool checkedWhile = true;
        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("1. Добавить клиентов");
            Console.WriteLine("2. Добавить самокатов");
            Console.WriteLine("3. Вывод списка клиентов");
            Console.WriteLine("4. Вывод списка самокатов");
            Console.WriteLine("5. Внесение данных об аренде");
            Console.WriteLine("6. Вывод списка аренд");
            if (SuperUser)
            {
                Console.WriteLine("7. Удаление клиентов из базы");
                Console.WriteLine("8. Изменение данных о Клиенте");
                Console.WriteLine("9. Изменение данных о Самокате");
            }
            Console.WriteLine("Любой другой символ - выход");

            Console.WriteLine();
            Console.Write("Действие: ");

            string? actionRead = Console.ReadLine();
            if (actionRead == null ||
                actionRead == string.Empty)
                continue;

            var action = actionRead.Parse<int>();

            Console.Clear();
            switch (action)
            {
                case 0:
                    checkedWhile = false;
                    break;
                case 1:
                    AddClient();
                    break;
                case 2:
                    AddScooter();
                    break;
                case 3:
                    ViewClients();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }
        } while (checkedWhile);
    }

    private static Permission Authorization()
    {
        Console.Title = "Авторизация";
        do
        {
            Console.Write("Логин: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = Until.NonVisibleReadLine();

            if (login == string.Empty ||
                password == string.Empty)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Есть пустые поля");
                Console.ResetColor();
                Console.WriteLine();
                continue;
            }

            string sql = "SELECT permission_id " +
                         "FROM users " +
                        $"WHERE login = '{login}' and " +
                              $"password = '{password}'";
            var list = sql.SQLQueryAsList<int>();

            if (list.Count == 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("▲ ");
                Console.WriteLine("Ошибка в Логине или Пароле!");
                Console.ResetColor();
                Console.WriteLine();
                continue;
            }

            Login = login;
            return (Permission)list.First();

        } while (true);
    }

    private static void AddClient()
    {
        string name, surname, numPhone;
        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Пример:");
            Console.WriteLine("Имя: Иван");
            Console.WriteLine("Имя: Иванович");
            Console.WriteLine("Номер телефона (+7): (800)555-35-35 /");
            Console.WriteLine("                      800 555 35 35 /");
            Console.WriteLine("                      8005553535 ");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Имя: ");
            name = Console.ReadLine();
            Console.Write("Фамилия: ");
            surname = Console.ReadLine();
            Console.Write("Номер телефона (+7): ");
            numPhone = new Regex(@"\d{10}").Match(Console.ReadLine().ToString()).Value;

            if (numPhone.Length != 10)
            {
                Console.WriteLine();
                Console.ForegroundColor= ConsoleColor.DarkRed;
                Console.WriteLine("Неправильный номер телефона");
                Thread.Sleep(1200);
                continue;
            }

            Console.Clear();
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");
            Console.WriteLine($"Номер телефона (+7): {numPhone}");

            Console.WriteLine();
            Console.Write("Правильно? [д/н] ");
            string? confirm = Console.ReadLine();
            if (confirm?.ToLower() == "д" ||
                confirm?.ToLower() == "y")
                break;
        } while (true);

        string sql = "INSERT INTO clients (name, surname, phone_number) " +
                     $"VALUES ('{name}', '{surname}', {numPhone.Parse<int>()})";
        sql.SQLNonQuery();
    }
    private static void AddScooter()
    {
        string name;
        while (true)
        {
            Console.Write("Название самоката: ");
            name = Console.ReadLine();

            if (name == string.Empty)
                continue;

            Console.WriteLine();
            Console.Write("Правильно? [д/н] ");
            string? confirm = Console.ReadLine();
            if (confirm?.ToLower() == "д" ||
                confirm?.ToLower() == "y")
                break;
        }

        string sql = "INSERT INTO scooter " +
                     $"VALUES ('{name}')";
        sql.SQLNonQuery();
    }

    private static void ViewClients()
    {
        string sql = "SELECT COUNT(*) " +
                     "FROM clients";
        int count = sql.SQLQueryAsList<int>().First();

        ConsoleKeyInfo key;
        while ((key = Console.ReadKey(true)).Key != ConsoleKey.Q)
        {
            int pageStart = 1;
            int pageEnd = 9;

            sql = "SELECT name, " +
                         "surname, " +
                         "phone_number " +
                  "FROM clients " +
                 $"WHERE id >= {pageStart} and " +
                       $"id <= {pageEnd}";
            List<string> list = sql.SQLQueryAsList<string>();

            Console.Clear();
            GridView(list, "name", "surname", "Phone Number");

            if (key.Key == ConsoleKey.B &&
                pageStart != 1)
            {
                pageStart -= 9;
                pageEnd -= 9;
            }
            else if (key.Key == ConsoleKey.N &&
                pageEnd != count)
            {
                pageStart += 9;
                pageEnd += 9;
            }
            else if (key.Key == ConsoleKey.Q)
                break;
        }
    }
    private static void GridView(List<string> list, params string[] columnsName)
    {
        for (int i = 0; i < columnsName.Count(); i++)
        {
            var num = columnsName[i].Length / 4 > 2 ? 2 : 1;
            string tab = string.Join("", Enumerable.Repeat("\t", num));
            Console.Write(columnsName[i] + tab);
        }
        Console.WriteLine();
        for (int i = 0; i < columnsName.Count(); i++)
        {
            string tire = string.Join("", Enumerable.Repeat("-", columnsName[i].Length + 1));
            var num = tire.Length / 4 > 2 ? 2 : 1;
            string tab = string.Join("", Enumerable.Repeat("\t", num));
            Console.Write(tire + tab);
        }

        Console.WriteLine();
        for (int i = 0; i < list.Count; i += columnsName.Length)
        {
            for (int a = 0; a < columnsName.Length; a++)
            {

                Console.Write(list[a+i] + string.Join("", Enumerable.Repeat("\t", counts[a])));
            }
            Console.WriteLine();
        }
    }
}