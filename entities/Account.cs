using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Account
    {
        public string AccountNumber { get; private set; }
        public string Branch { get; private set; }
        public decimal Balance { get; private set; }

        public Account()
        {
            AccountNumber = new Bogus.Faker().Random.ReplaceNumbers("######");
            Branch = "0001";
        }

        public Account(string accountNumber)
        {
            AccountNumber = accountNumber; //na hora que resetar o arquivo, seta o number
            Branch = "0001";
        }

        public void changeBalance(decimal value)
        {
            Balance += value;    
        }

    }
}
