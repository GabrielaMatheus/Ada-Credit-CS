using AdaCredit.enums;
using AdaCredit.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static AdaCredit.enums.Status;

namespace AdaCredit.Entities
{
    public sealed class Client
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public Account Account { get; private set; } = null;

        public Status.status status = status.Ativado;

        public Client() { }


        public override string ToString()
        {
            return "Nome:" + Name.ToString() + "\n"
                + "CPF:" + Document.ToString() + "\n"
                + "Nº da Conta:" + Account.AccountNumber.ToString() + "\n"
                + "Status:" + status.ToString() +"\n"
                + "Saldo:" + Account.Balance.ToString();
        }

        public Client(string name, string document)
        {
            Name = name;
            Document = document;
        }

        public Client(string name, string document, Account account)
        {
            Name = name;
            Document = document;
            Account = account;

        }

        
        public void changeName(string name)
        {
            Name = name;
        }
    }
}
