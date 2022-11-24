using LiteDB;
using System.Text;
using System.Data.Common;
using LiteDB;

namespace Arenda_Samokatov;

enum Permission
{
    Admin = 1,
    User = 2
}

internal static class Until
{
    private static string connectionDB = "Source.db";
    private static LiteDatabase DB = new LiteDatabase(connectionDB);

    internal static void Query<T>(this List<T> list)
    {

    }

    internal static T? Parse<T>(this string? value)
    {
        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch
        {
            return default;
        }
    }

    internal static string NonVisibleReadLine()
    {
        ConsoleKeyInfo key;
        StringBuilder sb = new StringBuilder();
        
        while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            sb.Append(key.KeyChar);

        return sb.ToString();
    }
}
