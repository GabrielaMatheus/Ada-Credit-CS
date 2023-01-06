using AdaCredit.Entities;
using AdaCredit.repositories;
using AdaCredit.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.controllers
{
    public class ClientController
    {
        public static void Add()
        {
            Console.WriteLine("Insira o nome do cliente:");
            string name = Console.ReadLine();

            Console.WriteLine("Insira o CPF sem números:");
            string document = Console.ReadLine();

            ClientService service = new ClientService();
            bool result = service.addClient(new Client(name, document));

            if (result)
                Console.WriteLine("Cliente cadastrado com Sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo Cliente.");

            Console.ReadKey();
        }

        public static void FetchByDocument()
        {
            Console.WriteLine("Insira o Nº do CPF:");
            string document = Console.ReadLine();

            ClientService service = new ClientService();
            Client result = service.getClientByDocument(document);
            Console.WriteLine(result);

            Console.ReadKey();
        }

        public static void Update()
        {
            Console.WriteLine("Insira o Documento sem números:");
            string document = Console.ReadLine();

            Console.WriteLine("Insira o novo Nome do cliente:");
            string newName = Console.ReadLine();

            ClientService service = new ClientService();
            Client client = service.getClientByDocument(document);

            Console.WriteLine("Deseja alterar o status? S | N");
            Console.WriteLine($"Status atual:  {client.status }");
            string status = Console.ReadLine();

            bool statusBool = false;
            if (status.ToUpper().Equals("S")) statusBool = true;

            bool result = service.updateClient(newName, document, statusBool);

            if (result)
                Console.WriteLine("Cliente atualizado com Sucesso!");
            else
                Console.WriteLine("Falha ao atualizar o Cliente.");

            Console.ReadKey();
        }

        public static void Deactivate() {
            Console.WriteLine("Insira o Documento sem números:");
            string document = Console.ReadLine();

            ClientService service = new ClientService();
            bool result = service.deactivateClient(document);

            if (result)
                Console.WriteLine("Cliente desativado com Sucesso!");
            else
                Console.WriteLine("Falha ao desativar o Cliente.");

            Console.ReadKey();
        }
    }
}
