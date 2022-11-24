using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
namespace Arenda_Samokatov.Data;

public class Client 
{
    public Client(int id, string surname, string name, string numbername)
    {
        Id = id;
        SurName = surname;
        Name = name;
        NumberPhone = numbername;
    }

    private string numberphone { get; set; }

    public int Id { get; set; }
    public string SurName { get; set; }
    public string Name { get; set; }
    public string NumberPhone 
    { 
        get => numberphone; 
        set {
            numberphone = value;
        }
    }
}
