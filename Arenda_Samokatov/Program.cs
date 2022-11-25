using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Arenda_Samokatov.Data;
using LiteDB;

namespace Arenda_Samokatov;

internal class Program
{
    public static string LoginName = string.Empty;
    private static bool SuperUser = false;

    public static void InsertData()
    {
        UsersExecution.table.DeleteAll();
        UsersExecution.table.Insert(ObjectId.NewObjectId(), new Users("Admin", "admin123", 1));
        UsersExecution.table.Insert(ObjectId.NewObjectId(), new Users("User", "user1234", 2));
    }

    public static void Main(string[] args)
    {
        InsertData();

        switch (UsersExecution.Auntification())
        {
            case Permission.Admin:
                Console.Title = $"Прокат самокатов - {LoginName} (Администратор)";
                SuperUser = true;
                break;
            case Permission.User:
                Console.Title = $"Прокат самокатов - {LoginName} (Пользователь)";
                break;
        }
        
        bool checkedWhile = true;
        while (checkedWhile)
        {
            Console.Clear();
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
            if (string.IsNullOrEmpty(actionRead))
                continue;

            var action = actionRead.Parse<int>();

            Console.Clear();
            //switch (action)
            //{
            //    case 0:
            //        checkedWhile = false;
            //        break;
            //    case 1:
            //        AddClient();
            //        break;
            //    case 2:
            //        AddScooter();
            //        break;
            //    case 3:
            //        ViewClients();
            //        break;
            //    case 4:
            //        ViewScooter();
            //        break;
            //    case 5:
            //        AddRent();
            //        break;
            //    case 6:
            //        ViewRent();
            //        break;
            //    case 7:
            //        break;
            //    case 8:
            //        if (!SuperUser)
            //            continue;
            //        EditClient();
            //        break;
            //    case 9:
            //        break;
            //}
        }
    }

    //private static void AddClient()
    //{
    //    string name, surname, numPhone;
    //    do
    //    {
    //        Console.Clear();
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.WriteLine("Пример:");
    //        Console.WriteLine("Имя: Иван");
    //        Console.WriteLine("Имя: Иванович");
    //        Console.WriteLine("Номер телефона (+7): (800)555-35-35 /");
    //        Console.WriteLine("                      800 555 35 35 /");
    //        Console.WriteLine("                      8005553535 ");
    //        Console.ResetColor();
    //        Console.WriteLine();

    //        Console.Write("Имя: ");
    //        name = Console.ReadLine();
    //        Console.Write("Фамилия: ");
    //        surname = Console.ReadLine();
    //        Console.Write("Номер телефона (+7): ");
    //        numPhone = new Regex(@"\d{10}").Match(Console.ReadLine().ToString()).Value;

    //        if (numPhone.Length != 10)
    //        {
    //            Console.WriteLine();
    //            Console.ForegroundColor= ConsoleColor.DarkRed;
    //            Console.WriteLine("Неправильный номер телефона");
    //            Thread.Sleep(1200);
    //            continue;
    //        }

    //        Console.Clear();
    //        Console.WriteLine($"Имя: {name}");
    //        Console.WriteLine($"Фамилия: {surname}");
    //        Console.WriteLine($"Номер телефона (+7): {numPhone}");

    //        Console.WriteLine();
    //        Console.Write("Правильно? [д/н] ");
    //        string? confirm = Console.ReadLine();
    //        if (confirm?.ToLower() == "д" ||
    //            confirm?.ToLower() == "y")
    //            break;
    //    } while (true);

    //    string sql = "INSERT INTO clients (name, surname, phone_number) " +
    //                 $"VALUES ('{name}', '{surname}', {numPhone.Parse<int>()})";
    //    sql.SQLNonQuery();
    //}
    //private static void AddScooter()
    //{
    //    string name;
    //    while (true)
    //    {
    //        Console.Write("Название самоката: ");
    //        name = Console.ReadLine();

    //        if (name == string.Empty)
    //            continue;

    //        Console.WriteLine();
    //        Console.Write("Правильно? [д/н] ");
    //        string? confirm = Console.ReadLine();
    //        if (confirm?.ToLower() == "д" ||
    //            confirm?.ToLower() == "y")
    //            break;
    //    }

    //    string sql = "INSERT INTO scooters (name) " +
    //                 $"VALUES ('{name}')";
    //    sql.SQLNonQuery();
    //}
    //private static void AddRent()
    //{
    //    int client = 1, scooter = 1;
    //    while (true)
    //    {
    //        string sql = "SELECT COUNT(*) " +
    //                     "FROM clients";
    //        int count = sql.SQLQueryAsList<int>().First();

    //        ConsoleKeyInfo key;
    //        bool checkWhile = true;
    //        int pageStart = 1;
    //        int pageEnd = 9;
    //        while (checkWhile)
    //        {
    //            sql = "SELECT id, " +
    //                         "name, " +
    //                         "surname, " +
    //                         "phone_number " +
    //                  "FROM clients " +
    //                 $"WHERE id >= {pageStart} and " +
    //                       $"id <= {pageEnd}";
    //            List<string> list = sql.SQLQueryAsList<string>();

    //            Console.Clear();
    //            GridView(list, "Номер", "Имя", "Фамилия", "Номер телефона");

    //            Console.WriteLine();
    //            Console.WriteLine("E - Ввести номер клиента\nB - Предыдущая страница\nN - Следующая страница\nQ - Выйти");

    //            key = Console.ReadKey(true);

    //            if (key.Key == ConsoleKey.B &&
    //                pageStart != 1)
    //            {
    //                pageStart -= 9;
    //                pageEnd -= 9;
    //            }
    //            else if (key.Key == ConsoleKey.N &&
    //                pageEnd <= count)
    //            {
    //                pageStart += 9;
    //                pageEnd += 9;
    //            }
    //            else if (key.Key == ConsoleKey.E)
    //            {
    //                Console.Write("Номер: ");
    //                client = Console.ReadLine().Parse<int>();
    //                break;
    //            }
    //            else if (key.Key == ConsoleKey.Q)
    //                return;
    //        }

    //        sql = "SELECT COUNT(*) " +
    //                     "FROM scooters";
    //        count = sql.SQLQueryAsList<int>().First();

    //        checkWhile = true;
    //        pageStart = 1;
    //        pageEnd = 9;
    //        while (checkWhile)
    //        {
    //            sql = "SELECT id, " +
    //                         "name " +
    //                  "FROM scooters " +
    //                 $"WHERE id >= {pageStart} and " +
    //                       $"id <= {pageEnd}";
    //            List<string> list = sql.SQLQueryAsList<string>();

    //            Console.Clear();
    //            GridView(list, "Номер", "Название");

    //            Console.WriteLine();
    //            Console.WriteLine("E - Ввести номер клиента\nB - Предыдущая страница\nN - Следующая страница\nQ - Выйти");

    //            key = Console.ReadKey(true);

    //            if (key.Key == ConsoleKey.B &&
    //                pageStart != 1)
    //            {
    //                pageStart -= 9;
    //                pageEnd -= 9;
    //            }
    //            else if (key.Key == ConsoleKey.N &&
    //                pageEnd <= count)
    //            {
    //                pageStart += 9;
    //                pageEnd += 9;
    //            }
    //            else if (key.Key == ConsoleKey.E)
    //            {
    //                Console.Write("Номер: ");
    //                scooter = Console.ReadLine().Parse<int>();
    //                break;
    //            }
    //            else if (key.Key == ConsoleKey.Q)
    //                return;
    //        }

    //        Console.Clear();

    //        sql = "SELECT clients.name, " +
    //                     "clients.surname, " +
    //                     "scooters.name " +
    //              "FROM clients LEFT JOIN " +
    //                    $"scooters on scooters.id = {scooter} " +
    //             $"WHERE clients.id = {client}";
    //        var _list = sql.SQLQueryAsList<string>();
    //        _list.Add(DateTime.Now.ToString());
    //        GridView(_list, "Имя", "Фамилия", "Скутер", "Время");
    //        Console.WriteLine("Правильно? [д/н] ");
    //        var cheeck = Console.ReadLine();
    //        if (cheeck?.ToLower() == "y" ||
    //            cheeck?.ToLower() == "д")
    //        {
    //            sql = "INSERT INTO rent (client_id, scooter_id, date) " +
    //                 $"VALUES ({client},{scooter},datetime('now'))";
    //            sql.SQLNonQuery();
    //        }
    //        else
    //            Console.Clear();
    //    }
    //}

    //private static void EditClient()
    //{
    //    string sql = "SELECT COUNT(*) " +
    //                 "FROM clients";
    //    int count = sql.SQLQueryAsList<int>().First();

    //    ConsoleKeyInfo key;
    //    bool checkWhile = true;
    //    int pageStart = 1;
    //    int pageEnd = 9;
    //    while (checkWhile)
    //    {

    //        sql = "SELECT id, " +
    //                     "name, " +
    //                     "surname, " +
    //                     "phone_number " +
    //              "FROM clients " +
    //             $"WHERE id >= {pageStart} and " +
    //                   $"id <= {pageEnd}";
    //        List<string> list = sql.SQLQueryAsList<string>();

    //        Console.Clear();
    //        GridView(list, "Номер", "Имя", "Фамилия", "Номер телефона");

    //        Console.WriteLine();
    //        Console.WriteLine("E - Ввести номер\nB - Предыдущая страница\nN - Следующая страница\nQ - Выйти");

    //        key = Console.ReadKey(true);

    //        if (key.Key == ConsoleKey.B &&
    //            pageStart != 1)
    //        {
    //            pageStart -= 9;
    //            pageEnd -= 9;
    //        }
    //        if (key.Key == ConsoleKey.E)
    //        {
    //            Console.WriteLine();
    //            Console.Write("Имя: ");
    //        }
    //        else if (key.Key == ConsoleKey.N &&
    //            pageEnd <= count)
    //        {
    //            pageStart += 9;
    //            pageEnd += 9;
    //        }
    //        else if (key.Key == ConsoleKey.Q)
    //            break;
    //    }
    //}

    //private static void ViewClients()
    //{
    //    string sql = "SELECT COUNT(*) " +
    //                 "FROM clients";
    //    int count = sql.SQLQueryAsList<int>().First();

    //    ConsoleKeyInfo key;
    //    bool checkWhile = true;
    //    int pageStart = 1;
    //    int pageEnd = 9;
    //    while (checkWhile)
    //    {

    //        sql = "SELECT id, " +
    //                     "name, " +
    //                     "surname, " +
    //                     "phone_number " +
    //              "FROM clients " +
    //             $"WHERE id >= {pageStart} and " +
    //                   $"id <= {pageEnd}";
    //        List<string> list = sql.SQLQueryAsList<string>();

    //        Console.Clear();
    //        GridView(list, "Номер", "Имя", "Фамилия", "Номер телефона");

    //        Console.WriteLine();
    //        Console.WriteLine("B - предыдущая страница\tQ - Выйти\tN - Следующая страница");

    //        key = Console.ReadKey(true);

    //        if (key.Key == ConsoleKey.B &&
    //            pageStart != 1)
    //        {
    //            pageStart -= 9;
    //            pageEnd -= 9;
    //        }
    //        else if (key.Key == ConsoleKey.N &&
    //            pageEnd <= count)
    //        {
    //            pageStart += 9;
    //            pageEnd += 9;
    //        }
    //        else if (key.Key == ConsoleKey.Q)
    //            break;
    //    }
    //}
    //private static void ViewScooter()
    //{
    //    string sql = "SELECT COUNT(*) " +
    //                         "FROM scooters";
    //    int count = sql.SQLQueryAsList<int>().First();

    //    ConsoleKeyInfo key;
    //    bool checkWhile = true;
    //    int pageStart = 1;
    //    int pageEnd = 9;
    //    while (checkWhile)
    //    {

    //        sql = "SELECT id, " +
    //                     "name " +
    //              "FROM scooters " +
    //             $"WHERE id >= {pageStart} and " +
    //                   $"id <= {pageEnd}";
    //        List<string> list = sql.SQLQueryAsList<string>();

    //        Console.Clear();
    //        GridView(list, "Номер", "Название");
    //        Console.WriteLine();
    //        Console.WriteLine("B - предыдущая страница\tQ - Выйти\tN - Следующая страница");

    //        key = Console.ReadKey(true);

    //        if (key.Key == ConsoleKey.B &&
    //            pageStart != 1)
    //        {
    //            pageStart -= 9;
    //            pageEnd -= 9;
    //        }
    //        else if (key.Key == ConsoleKey.N &&
    //            pageEnd <= count)
    //        {
    //            pageStart += 9;
    //            pageEnd += 9;
    //        }
    //        else if (key.Key == ConsoleKey.Q)
    //            break;
    //    }
    //}
    //private static void ViewRent()
    //{

    //    string sql = "SELECT COUNT(*) " +
    //                 "FROM rent";
    //    int count = sql.SQLQueryAsList<int>().First();

    //    ConsoleKeyInfo key;
    //    bool checkWhile = true;
    //    int pageStart = 1;
    //    int pageEnd = 9;
    //    while (checkWhile)
    //    {

    //        sql = "SELECT clients.name, " +
    //                     "clients.surname, " +
    //                     "clients.phone_number, " +
    //                     "scooter.name, " +
    //                     "rent.date" +
    //              "FROM rent INNER JOIN " +
    //                     "clients ON rent.client_id = clients.id INNER JOIN " +
    //                     "scooters ON rent.scooter_id = scooters.id" +
    //             $"WHERE id >= {pageStart} and " +
    //                   $"id <= {pageEnd}";
    //        List<string> list = sql.SQLQueryAsList<string>();

    //        Console.Clear();
    //        GridView(list, "Имя", "Фамилия", "Номер телефона", "Самокат", "Дата");

    //        Console.WriteLine();
    //        Console.WriteLine("B - предыдущая страница\tQ - Выйти\tN - Следующая страница");

    //        key = Console.ReadKey(true);

    //        if (key.Key == ConsoleKey.B &&
    //            pageStart != 1)
    //        {
    //            pageStart -= 9;
    //            pageEnd -= 9;
    //        }
    //        else if (key.Key == ConsoleKey.N &&
    //            pageEnd <= count)
    //        {
    //            pageStart += 9;
    //            pageEnd += 9;
    //        }
    //        else if (key.Key == ConsoleKey.Q)
    //            break;
    //    }
    //}

    //private static void GridView(List<string> list, params string[] columnsName)
    //{
    //    int[] maxCount = new int[columnsName.Length];
    //    for (int i = 0; i < list.Count; i += columnsName.Length)
    //        for (int a = 0; a < columnsName.Length; a++)
    //            maxCount[a] = list[a + i].Length > maxCount[a] ? list[a + i].Length : maxCount[a];

    //    for (int i = 0; i < columnsName.Count(); i++)
    //    {
    //        var num = tabCount(maxCount[i], columnsName[i].Length);
    //        string str = columnsName[i] + string.Join("", Enumerable.Repeat("\t", num));
    //        Console.Write(str);
    //    }

    //    Console.WriteLine();
    //    for (int i = 0; i < columnsName.Count(); i++)
    //    {
    //        string tire = string.Join("", Enumerable.Repeat("-", columnsName[i].Length)); 
    //        string tab = string.Join("", Enumerable.Repeat("\t", tabCount(maxCount[i], columnsName[i].Length)));
    //        Console.Write(tire + tab);
    //    }

    //    Console.WriteLine();
    //    for (int i = 0; i < list.Count; i += columnsName.Length)
    //    {
    //        for (int a = 0; a < columnsName.Length; a++)
    //        {
    //            var num = tabCount(maxCount[a], list[a+i].Length);
    //            Console.Write(list[a+i] + string.Join("", Enumerable.Repeat("\t", num)));
    //        }
    //        Console.WriteLine();
    //    }
    //}
    //private static int tabCount(int max, int value) 
    //{
    //    int stat = 0;
    //    var temp = (max + (4 - max % 4)) - value;
    //    while (true)
    //    {
    //        if (temp <= 4)
    //        {
    //            ++stat;
    //            break;
    //        }
    //        else
    //        {
    //            ++stat;
    //            temp -= 4;
    //        }
    //    }
    //    return stat;
    //}
}