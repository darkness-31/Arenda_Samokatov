using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arenda_Samokatov.Data;

public class Client
{
    public Client(int id, string SurName, string Name, string NumberName)
    {
        Id = id;
        this.SurName = SurName;
        this.Name = Name;
        NumberPhone = NumberName;
    }

    private string numberphone { get; set; }
    private string name { get; set; }
    private string surname { get; set; }

    public int Id { get; set; }
    public string SurName
    {
        get => surname;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Пустое значение");

            if (value.Length <= 2)
                throw new Exception("Длина имени меньше 2 символов");

            surname = value;
        }
    }
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Пустое значение");

            if (value.Length <= 2)
                throw new Exception("Длина имени меньше 2 символов");

            name = value;
        }
    }
    public string NumberPhone
    {
        get => numberphone;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Пустое значение");

            Regex rg = new Regex(@"((?:\(?[2-9](?:(?=1)1[02-9]|(?:(?=0)0[1-9]|\d{2}))\)?\D{0,3})(?:\(?[2-9](?:(?=1)1[02-9]|\d{2})\)?\D{0,3})\d{4})");
            var match = rg.Match(value);
            if (match.Length == 0)
                throw new Exception("Не номер телефона");

            numberphone = value;
        }
    }
}
