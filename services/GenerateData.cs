using AdaCredit.controllers;
using AdaCredit.entities;
using AdaCredit.Entities;
using AdaCredit.enums;
using AdaCredit.repositories;
using AdaCredit.structs;
using Bogus;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;


namespace AdaCredit.services
{
    public static class GenerateData
    {
        public static void Populate()
        {
            Bogus.Faker faker = new Bogus.Faker();
            ClientService clientService = new ClientService();
            Client c = new Client();
            TransactionRepository t = new TransactionRepository();  

            for (int i = 0; i < 5; i++)
            {
                string name = faker.Name.FullName();
                string document = faker.Random.ReplaceNumbers("###########");
                var account = new Account();
                c = new Client(name, document, account);
                clientService.addClient(c);

                //saving in transactions

                int originBank = 777;
                string sourceBankAgency = "0001";
                int sourceBankAccount = int.Parse(account.AccountNumber);
                int sourceBankDestiny = 777;
                string destinyBankAgency = faker.Random.ReplaceNumbers("####");
                int destinyBankAccount = int.Parse(faker.Random.ReplaceNumbers("######"));
                Enum.TryParse<TransactionType>(faker.PickRandom(new string[] { "TED", "DOC", "TEF" }), out TransactionType transactionType);
                Enum.TryParse<TypeWay>(faker.PickRandom(new string[] { "0", "1" }), out TypeWay typeWay);
                decimal valueNumber = decimal.Parse(faker.Random.ReplaceNumbers("###.##"));
                DateTime date = faker.Date.Recent(60);
                string bankingName = faker.Name.LastName();

                for(int j = 0; j < 1; j++)
                {
                    int originBank2 = 777;
                    string sourceBankAgency2 = "0001";
                    int sourceBankAccount2 = int.Parse(account.AccountNumber);
                    int sourceBankDestiny2 = 777;
                    string destinyBankAgency2 = faker.Random.ReplaceNumbers("####");
                    int destinyBankAccount2 = int.Parse(faker.Random.ReplaceNumbers("######"));
                    Enum.TryParse<TransactionType>(faker.PickRandom(new string[] { "TED", "DOC", "TEF" }), out TransactionType transactionType2);
                    Enum.TryParse<TypeWay>(faker.PickRandom(new string[] { "0", "1" }), out TypeWay typeWay2);
                    decimal valueNumber2 = decimal.Parse(faker.Random.ReplaceNumbers("###.##"));

                    TransactionRepository.transactions.Add(TransactionRepository.Create(originBank2
                        , sourceBankAgency2, sourceBankAccount2, sourceBankDestiny2,
                        destinyBankAgency2, destinyBankAccount2, transactionType2, typeWay2
                        , valueNumber2, date, bankingName));

                }

                TransactionRepository.transactions.Add(TransactionRepository.Create(originBank, sourceBankAgency, sourceBankAccount, sourceBankDestiny,
                    destinyBankAgency, destinyBankAccount, transactionType, typeWay, valueNumber, date, bankingName));
            }

            Save(TransactionRepository.transactions);
            TransactionRepository.transactions.Clear();


        }

        public static void Save(List<Transaction> t)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            t = t.OrderBy(d => d.Date).ToList();

            if (t.Count() == 0) return;

            string dateTime = t[0].Date.ToString("yyyyMMdd");
            string bankingName = t[0].BankingName;

            foreach (Transaction transaction in t)
            {
                TransactionDTO structTransaction = new TransactionDTO(transaction.SourceBankCode
                    ,transaction.SourceBankAgency
                    ,transaction.SourceBankAccount
                    ,transaction.DestinyBankCode
                    ,transaction.DestinyBankAgency
                    ,transaction.DestinyBankAccount
                    ,transaction.TransactionType
                    ,transaction.TypeWay
                    ,transaction.ValueNumber);


                if (!(transaction.BankingName == bankingName && transaction.Date.ToString("yyyyMMdd") == dateTime))
                {
                    dateTime = transaction.Date.ToString("yyyyMMdd");
                    bankingName = transaction.BankingName;
                }


                string folderName = $@"{desktopPath}\Transactions\Pending";
                System.IO.Directory.CreateDirectory(folderName);
                string pathString = Path.Combine(folderName, $"{bankingName}-{dateTime}.txt");

                using (StreamWriter writer = new StreamWriter(pathString,true))
                using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(structTransaction);
                    csv.NextRecord();
                    writer.Flush();
                }
            }

        }
    }
}
