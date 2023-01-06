using AdaCredit.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.structs
{
    public struct TransactionDTO
    {
        public int SourceBankCode { get; set; }
        public string SourceBankAgency { get; set; }
        public int SourceBankAccount { get; set; }
        public int DestinyBankCode { get; set; }
        public string DestinyBankAgency { get; set; }
        public int DestinyBankAccount { get; set; }
        public TransactionType TransactionType { get; set; }
        public int TypeWay { get; set; }
        public decimal ValueNumber { get; set; }

        public TransactionDTO(int sourceBankCode, string sourceBankAgency, int sourceBankAccount, int destinyBankCode, string destinyBankAgency, int destinyBankAccount, TransactionType transactionType, TypeWay typeWay, decimal valueNumber)
        {
            SourceBankCode = sourceBankCode;
            SourceBankAgency = sourceBankAgency;
            SourceBankAccount = sourceBankAccount;
            DestinyBankCode = destinyBankCode;
            DestinyBankAgency = destinyBankAgency;
            DestinyBankAccount = destinyBankAccount;
            TransactionType = transactionType;
            TypeWay = (int)typeWay;
            ValueNumber = valueNumber;
        }

        public override string ToString()
        {
            return $"Código do banco de origem: {SourceBankCode}\n" +
                $"Agência do banco de origem: {SourceBankAgency}\n" +
                $"Conta do banco de origem: {SourceBankAccount}\n " +
                $"Código do banco de destino: {DestinyBankCode}\n" +
                $"Agência do banco de destino: {DestinyBankAgency}\n " +
                $"Conta do banco de destino: {DestinyBankAccount}\n" +
                $"Tipo da transação: {TransactionType}\n" +
                $"Sentido da Transação: {TypeWay}\n" +
                $"Valor: {ValueNumber}";
        }
    }
}
