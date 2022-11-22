using System.Data.SqlTypes;
using System.Text;

namespace Arenda_Samokatov;

enum Permission
{
    Admin = 1,
    User = 2
}

internal static class Until
{
    private static string connectionDB = "Data Source=Source.db";
    private static SqliteConnection DB = new SqliteConnection(connectionDB);

    internal static void SQLNonQuery(this string sql)
    {
        DB.Open();
        SqliteCommand command = DB.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
        DB.Close();
    }
    internal static List<T> SQLQueryAsList<T>(this string sql)
    {
        var list = new List<T>();

        if (sql == string.Empty)
            return list;

        DB.Open();
        SqliteCommand command = DB.CreateCommand();
        command.CommandText = sql;
        SqliteDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    list.Add(reader[i].ToString().Parse<T>());
            }
        }
        DB.Close();

        return list;
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
