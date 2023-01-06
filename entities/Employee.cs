using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdaCredit.enums.Status;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using AdaCredit.enums;


namespace AdaCredit.Entities
{
    public sealed class Employee
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public enums.Status.status status = status.Ativado;

        public DateTime LastLogin { get; private set; }



        public Employee(){}

        public override string ToString()
        {
            return "Nome:" + Name.ToString() + "\n"
                + "CPF:" + Document.ToString() + "\n"
                + "Último Login:" + LastLogin.ToString();
        }

        public Employee(string name, string document,string username, string password)
        {
            Name = name;
            Document = document;
            Username = username;
            Password = password;
        }

        public void changePassword(string password)
        {
            Password = password;
        }

        public void changeLastLogin()
        {
            LastLogin = DateTime.Now;
        }
    }
}
