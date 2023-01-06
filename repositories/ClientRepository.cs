using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdaCredit.Entities;
using AdaCredit.enums;
using AdaCredit.services;
using AdaCredit.structs;
using Bogus.DataSets;
using CsvHelper;
using Microsoft.Graph;
using static AdaCredit.enums.Status;
using static Azure.Core.HttpHeader;

namespace AdaCredit.repositories
{
    public class ClientRepository
    {
        public static List<Client> clients = new List<Client>();

        public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public ClientRepository() { }

        private void Save()
        {
            string filePath = Path.Combine(desktopPath, "clients.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(clients);
                writer.Flush();
            }
        }

        public Client GetByDocument(string document)
        {
            if (clients.Count == 0) return null;

            foreach (Client client in clients)
                if (client.Document.Equals(document))return client;

            return null;
        }

        public Client GetByAccountNumber(string accountNumber)
        {
            if (clients.Count == 0) return null;

            foreach (Client client in clients)
                if (client.Account.AccountNumber.Equals(accountNumber))return client;

            return null;
        }


        public Client StatusDeactivated()
        {
            if (clients.Count == 0) return null;

            foreach (Client client in clients)
            {
                if(client.status.Equals(status.Desativado))
                {
                    Console.WriteLine(client);
                    continue;
                }
            }
            return null;
        }

        public Client StatusActiveAndBalance()
        {
            if (clients.Count == 0) return null;

            foreach (Client client in clients)
            {
                if (client.status.Equals(status.Desativado))
                {
                    Console.WriteLine(client);
                    continue;
                }
            }
            return null;
        }

        public bool Add(Client client)
        {
            if (clients.Count == 0)
            {
                Client entity1 = new Client(client.Name, client.Document, client.Account);
                clients.Add(entity1);

                Save();

                return true;
            }
            if(clients.Any(x => x.Document.Equals(client.Document))){
                Console.WriteLine("Cliente já cadastrado.");
                Console.ReadKey();

                return false;

            }
            Client entity2 = new Client(client.Name, client.Document, client.Account);
            clients.Add(entity2);

            Save();

            return true;
        }

        public bool UpdateClientName(string name, string document, bool status)
        {
            Client clientAccount = GetByDocument(document);

            if (clientAccount == null) return false;

            clientAccount.changeName(name);

            if (status)
            {
                if(clientAccount.status == enums.Status.status.Ativado)
                {
                    clientAccount.status = enums.Status.status.Desativado;
                }
                else{
                    clientAccount.status = enums.Status.status.Ativado;
                }
            }

            Save();
            return true;
        }

        public bool Deactivate(string document)
        {
            Client clientAccount = GetByDocument(document);

            if (clientAccount == null) return false;

            clientAccount.status = status.Desativado;
            Save();
            return true;
        }

        public Client Activate()
        {
            if (clients.Count == 0) return null;

            foreach (Client client in clients)
            {
                if (client.status.Equals(status.Ativado))
                {
                    Console.WriteLine(client);
                    continue;
                }
            }
            return null;
        }

        public static void LoadClients()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
           
                var pathFile = Path.Combine(desktopPath, "clients.txt");

            if (System.IO.File.Exists(pathFile))
            {
                string[] linhas = System.IO.File.ReadAllLines(pathFile);

                for (int i = 1; i < linhas.Length; i++)
                {
                    string[] dados = linhas[i].Split(',');
                    

                    string name = dados[0];
                    string document = dados[1];

                    Account account = new Account(dados[2]);
                    account.changeBalance(decimal.Parse(dados[4]));

                    Client client = new Client(name, document, account);
                    ClientService cs = new ClientService();
                    cs.addClient(client);

                    

                }
            };
        }
    }
}
