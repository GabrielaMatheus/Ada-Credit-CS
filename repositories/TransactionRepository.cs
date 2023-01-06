using AdaCredit.controllers;
using AdaCredit.entities;
using AdaCredit.Entities;
using AdaCredit.enums;
using AdaCredit.structs;
using CsvHelper;
//using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = AdaCredit.entities.Transaction;

namespace AdaCredit.repositories
{
    public class TransactionRepository
    {
        public static List<Transaction> transactions = new List<Transaction>();

        public static List<Transaction> failTransactions = new List<Transaction>();
        public static List<Transaction> successTransactions = new List<Transaction>();

        public static void LoadTransactions()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            docPath += "\\Transactions";

            string dirPending = $@"{docPath}\Pending";
            string dirCompleted = $@"{docPath}\Completed";
            string dirFailed = $@"{docPath}\Failed";

            List<string> strPaths = new List<string>();
            strPaths.Add(dirPending);
            strPaths.Add(dirCompleted);
            strPaths.Add(dirFailed);

            foreach (string path in strPaths)
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] files = Directory.GetFiles(dirPending);

            foreach (string file in files)
            {
                string[] fileName = Path.GetFileNameWithoutExtension(file).Split('-');
                if (fileName.Count() < 2) return;

                string name = fileName[0];
                string date = fileName[1];

                DateTime formatedDate = DateTime.ParseExact(date, "yyyyMMdd",CultureInfo.InvariantCulture);
                
                ScrollsFile(file, formatedDate, name);

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string folderName = $@"{desktopPath}\Transactions\Failed";
                System.IO.Directory.CreateDirectory(folderName);
                string pathString = Path.Combine(folderName, $"{name}-{date}.txt");

                using (StreamWriter writer = new StreamWriter(pathString, true))
                using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(failTransactions);
                    csv.NextRecord();
                    writer.Flush();


                    csv.WriteRecords(successTransactions);
                    csv.NextRecord();
                    writer.Flush();

                }
            }

            System.IO.Directory.Delete(dirPending, true);

        }

        public static void ViewFailedFiles()
        {
            if (failTransactions.Count >= 0)
            {
                TransactionDTO tDTO; 
                foreach (var transaction in failTransactions)
                {
                    tDTO = new TransactionDTO(transaction.SourceBankCode, transaction.SourceBankAgency, transaction.SourceBankAccount, transaction.DestinyBankCode, transaction.DestinyBankAgency, transaction.DestinyBankAccount, transaction.TransactionType, transaction.TypeWay, transaction.ValueNumber);

                    tDTO.ToString();
                }
                return;
            }

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderName = $@"{desktopPath}\Transactions\Failed";
            string[] files = Directory.GetFiles(folderName);

            foreach (string file in files)
            {
                //extract the name
                string[] fileName = Path.GetFileNameWithoutExtension(file).Split('-');
                if (fileName.Count() < 2) return;

                string name = fileName[0];
                string date = fileName[1];

                DateTime formatedDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

                //scroll through the file
                var pathFile = Path.Combine(folderName, $"{name}-{date}.txt");
                string[] linhas = File.ReadAllLines(pathFile);

                for (int i = 0; i < linhas.Length; i++)
                {
                    string[] dados = linhas[i].Split(',');

                    int.TryParse(dados[0], out int sourceBankCode);
                    string sourceBankAgency = dados[1];
                    int.TryParse(dados[2], out int sourceBankAccount);
                    int.TryParse(dados[3], out int destinyBankCode);
                    string destinyBankAgency = dados[4];
                    int.TryParse(dados[5], out int destinyBankAccount);
                    Enum.TryParse<TransactionType>(dados[6], out TransactionType transactionType);
                    Enum.TryParse<TypeWay>(dados[7], out TypeWay typeWay);
                    decimal.TryParse(dados[8], out decimal valueNumber);

                    Transaction t = new Transaction(sourceBankCode, sourceBankAgency, sourceBankAccount, destinyBankCode, destinyBankAgency
                        , destinyBankAccount, transactionType, typeWay, valueNumber, DateTime.Parse(date), name);

                    failTransactions.Add(t);

                    Console.WriteLine(t.ToString());
                }
            }
        }

        public static void ViewCompletedFiles()
        {
            if (successTransactions.Count >= 0)
            {
                TransactionDTO tDTO; 
                foreach (var transaction in successTransactions)
                {
                    tDTO = new TransactionDTO(transaction.SourceBankCode, transaction.SourceBankAgency, transaction.SourceBankAccount, transaction.DestinyBankCode, transaction.DestinyBankAgency, transaction.DestinyBankAccount, transaction.TransactionType, transaction.TypeWay, transaction.ValueNumber);

                    tDTO.ToString();
                }
                return;
            }

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderName = $@"{desktopPath}\Transactions\Completed";
            string[] files = Directory.GetFiles(folderName);

            foreach (string file in files)
            {
                //extract the name
                string[] fileName = Path.GetFileNameWithoutExtension(file).Split('-');
                if (fileName.Count() < 2) return;

                string name = fileName[0];
                string date = fileName[1];

                DateTime formatedDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

                //scroll through the file
                var pathFile = Path.Combine(folderName, $"{name}-{date}.txt");
                string[] linhas = File.ReadAllLines(pathFile);

                for (int i = 0; i < linhas.Length; i++)
                {
                    string[] dados = linhas[i].Split(',');

                    int.TryParse(dados[0], out int sourceBankCode);
                    string sourceBankAgency = dados[1];
                    int.TryParse(dados[2], out int sourceBankAccount);
                    int.TryParse(dados[3], out int destinyBankCode);
                    string destinyBankAgency = dados[4];
                    int.TryParse(dados[5], out int destinyBankAccount);
                    Enum.TryParse<TransactionType>(dados[6], out TransactionType transactionType);
                    Enum.TryParse<TypeWay>(dados[7], out TypeWay typeWay);
                    decimal.TryParse(dados[8], out decimal valueNumber);

                    Transaction t = new Transaction(sourceBankCode, sourceBankAgency, sourceBankAccount, destinyBankCode, destinyBankAgency
                        , destinyBankAccount, transactionType, typeWay, valueNumber, DateTime.Parse(date), name);

                    successTransactions.Add(t);
                }
            }
        }

        public static void ScrollsFile(string path, DateTime date, string bankingName)
        {
            string[] linhas = File.ReadAllLines(path);

            for (int i = 0; i < linhas.Length; i++)
            {
                string[] dados = linhas[i].Split(',');

                if (dados.Count() < 9) return;

                int.TryParse(dados[0], out int sourceBankCode);
                string sourceBankAgency = dados[1];
                int.TryParse(dados[2], out int sourceBankAccount);
                int.TryParse(dados[3], out int destinyBankCode);
                string destinyBankAgency = dados[4];
                int.TryParse(dados[5], out int destinyBankAccount);
                Enum.TryParse<TransactionType>(dados[6], out TransactionType transactionType);
                Enum.TryParse<TypeWay>(dados[7], out TypeWay typeWay);
                //TypeWay typeWay = TypeWay.INVALID;

                if(!(typeWay == TypeWay.Debit || typeWay == TypeWay.Credit)) typeWay = TypeWay.INVALID;

                decimal.TryParse(dados[8], out decimal valueNumber);

                Transaction transaction = new Transaction(destinyBankAccount, sourceBankAgency, sourceBankAccount, destinyBankCode, destinyBankAgency, destinyBankAccount, transactionType, typeWay, valueNumber, date, bankingName);

                //Verificar se o tipo de 'typeWay' ou 'transactionType' é inválido, conforme lido do arquivo.
                if (transaction.TypeWay == TypeWay.INVALID || transaction.TransactionType == TransactionType.INVALID)
                {
                    AddToFailTransactions(transaction);
                    continue;
                }

                if(transaction.ValueNumber <= 0)
                {
                    AddToFailTransactions(transaction);
                    continue;
                }

                if (sourceBankCode <= 0 || sourceBankAccount <= 0 || destinyBankCode <= 0 || destinyBankAccount <= 0)
                {
                    AddToFailTransactions(transaction);
                    continue;
                }

                TransactionsController transactionsController = new TransactionsController();
                transactionsController.TariffsControll(transaction);

                if (transaction.TransactionType == TransactionType.TEF)
                {
                    bool destinyAccount = AccountRepository.accounts.Any(x => x.AccountNumber.Equals(destinyBankAccount));
                    bool destinyAgency = transaction.DestinyBankAgency == "0001";

                    if (!destinyAccount && !destinyAgency)
                    {
                        AddToFailTransactions(transaction);
                        continue;
                    }

                    ClientRepository accountRepository = new ClientRepository();

                    Client clientSourceAccount = accountRepository.GetByAccountNumber(transaction.SourceBankAccount.ToString());

                    Client clientDestinyAccount = accountRepository.GetByAccountNumber(transaction.SourceBankAccount.ToString());

                    if (transaction.TypeWay == TypeWay.Credit)
                    {
                        if (!(clientDestinyAccount.Account.Balance >= transaction.ValueNumber + transaction.TaxFee))
                        {
                            AddToFailTransactions(transaction);
                            continue;
                        }

                        clientDestinyAccount.Account.changeBalance(- transaction.ValueNumber + transaction.TaxFee);
                        clientSourceAccount.Account.changeBalance(transaction.ValueNumber);
                    }else if (transaction.TypeWay == TypeWay.Debit)
                    {
                        if (!(clientSourceAccount.Account.Balance >= transaction.ValueNumber))
                        {
                            AddToFailTransactions(transaction);
                            continue;
                        }

                        clientSourceAccount.Account.changeBalance(- transaction.ValueNumber);
                        clientSourceAccount.Account.changeBalance(transaction.ValueNumber);
                    }

                    successTransactions.Add(transaction);
                    continue;
                }
                else if (transaction.TransactionType == TransactionType.DOC || transaction.TransactionType == TransactionType.TED)
                {
                    bool destinyAccountFinded = AccountRepository.accounts.Any(x => x.AccountNumber.Equals(destinyBankAccount));
                    bool destinyAgency = transaction.DestinyBankAgency == "0001";

                    bool sourceAccountFinded = AccountRepository.accounts.Any(x => x.AccountNumber.Equals(sourceBankAccount));
                    bool sourceAgencyBool = transaction.DestinyBankAgency == "0001";

                    if (!((destinyAccountFinded && destinyAgency) ^ (sourceAccountFinded && sourceAgencyBool)))
                    {
                        AddToFailTransactions(transaction);
                        continue;
                    }

                    if (transaction.TypeWay == TypeWay.Credit)
                    {
                        if (destinyAccountFinded)
                        {
                            ClientRepository accountRepository = new ClientRepository();
                            Client clientDestinyAccount = accountRepository.GetByAccountNumber(transaction.SourceBankAccount.ToString());

                            clientDestinyAccount.Account.changeBalance(transaction.ValueNumber);
                        }else if(sourceAccountFinded)
                        {
                            ClientRepository accountRepository = new ClientRepository();
                            Client clientSourceAccount = accountRepository.GetByAccountNumber(transaction.SourceBankAccount.ToString());

                            if (!(clientSourceAccount.Account.Balance >= transaction.ValueNumber + transaction.TaxFee))
                            {
                                AddToFailTransactions(transaction);
                                continue;
                            }

                            clientSourceAccount.Account.changeBalance(- transaction.ValueNumber + transaction.TaxFee);
                        }
                    }
                   
                    successTransactions.Add(transaction);
                    continue;
                }
            }
        }
    
        private static void AddToFailTransactions(Transaction t)
        {
            failTransactions.Add(t);
        }

        public static Transaction Create(int sourceBankCode, string sourceBankAgency, int sourceBankAccount, int destinyBankCode, string destinyBankAgency, int destinyBankAccount, TransactionType transactionType, TypeWay typeWay, decimal valueNumber, DateTime date, string bankingName){
            Transaction t = new Transaction(
                sourceBankCode,
                sourceBankAgency,
                sourceBankAccount,
                destinyBankCode,
                destinyBankAgency,
                destinyBankAccount,
                transactionType,
                typeWay,
                valueNumber,
                date,
                bankingName
            );

            if(t.TaxFee == 0) new TransactionsController().TariffsControll(t);

            return t;
        }
    }
}
