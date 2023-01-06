using AdaCredit.entities;
using AdaCredit.enums;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AdaCredit.repositories;
using Transaction = AdaCredit.entities.Transaction;

namespace AdaCredit.controllers
{
    public class TransactionsController
    {
        public static void Execute()
        {
            TransactionRepository.LoadTransactions();
            

        }

        public void TariffsControll(Transaction transaction)
        {
            DateTime dateDefault = new DateTime(2022, 11, 30, 0, 0, 0); ;

            if(transaction.TypeWay == TypeWay.Credit) transaction.TaxFee = 0;
            else if(transaction.TypeWay == TypeWay.Debit)
            {
                int compare = DateTime.Compare(transaction.Date, dateDefault);

                if(compare <= 0) transaction.TaxFee = 0;
                else
                {
                    if (transaction.TransactionType == TransactionType.DOC)
                    {
                        const decimal FIXED_FEE = 1;
                        const decimal MAX_FEE = 5;

                        decimal dinamicFee = transaction.ValueNumber / 100;
                        transaction.TaxFee = dinamicFee >= 5 ? MAX_FEE + FIXED_FEE : dinamicFee + FIXED_FEE;
                    }
                    else if(transaction.TransactionType == TransactionType.TED) transaction.TaxFee = 5;
                    else if(transaction.TransactionType == TransactionType.TEF) transaction.TaxFee = 0;
                }
            }
        }
    }
}
