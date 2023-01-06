using AdaCredit.Entities;
using AdaCredit.repositories;
using AdaCredit.validation;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.services
{
    public class ClientService
    {
        ClientRepository clientRepository = new ClientRepository();
        
        public Client getClientByDocument(string document)
        {
                return clientRepository.GetByDocument(document);
        }

        public bool addClient(Client client)
        {
            return clientRepository.Add(client);
        }

        public bool updateClient(string newName, string document, bool status)
        {
            return clientRepository.UpdateClientName(newName, document, status);
        }

        public bool deactivateClient(string document)
        {
            return clientRepository.Deactivate(document);
        }

        public Client statusDeactivated()
        {
            return clientRepository.StatusDeactivated();
        }

        public Client clientActivate()
        {
            return clientRepository.Activate();
        }
    }
}
