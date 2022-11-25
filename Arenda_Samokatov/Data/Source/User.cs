using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arenda_Samokatov.Data
{
    public class User
    {
        internal User(string? Login, string? Password, int AccessID)
        {
            this.Login = Login;
            this.Password = Password;
            this.Access = AccessID;
        }

        private string login { get; set; }
        private string password { get; set; }
        private Permission access { get; set; }

        public string Login 
        { 
            get => login;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Пустое значение");
                if (value.Length < 4)
                    throw new Exception("Логин меньше 4 символов");

                login = value;
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Пустое значение");
                if (value.Length < 8)
                    throw new Exception("Пароль меньше 8 символов");

                password = value;
            }
        }
        public int Access 
        {
            get => (int)access;
            set
            {
                access = (Permission)value;
            }
        }
    }
}
