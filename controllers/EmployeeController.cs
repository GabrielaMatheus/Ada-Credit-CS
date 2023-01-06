using AdaCredit.Entities;
using AdaCredit.repositories;
using AdaCredit.services;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Bogus;

namespace AdaCredit.controllers
{
    public class EmployeeController
    {
        public static void Add()
        {
            Console.WriteLine("Cadastro de Funcionário.");

            Console.WriteLine("Insira o nome do funcionário:");
            string name = Console.ReadLine();

            Console.WriteLine("Insira o documento do funcionário:");
            string document = Console.ReadLine();

            Console.WriteLine("Insira a senha:");
            string password = Console.ReadLine();

            bool confirmation = false;

            do
            {
                Console.WriteLine("Insira a senha novamente:");
                string passwordConfirmation = Console.ReadLine();

                confirmation = password.Equals(passwordConfirmation);

            } while (!confirmation);


            EmployeeRepository repository = new EmployeeRepository();

            int aleatoryNumber = new Bogus.Faker().Random.Number(3);
            string username = name.Split(" ")[0] + aleatoryNumber;

            string hashedPassword = HashPassword(password, GenerateSalt(12));

            bool result = repository.Add(new Employee(name, document, username, hashedPassword));

            if (result)
                Console.WriteLine("Funcionário cadastrado com Sucesso! \n  " +
                    $"\n Usuário: {username}\n");
            
            else
                Console.WriteLine("Falha ao cadastrar novo Funcionário.");


            Console.ReadKey();
        }

        public static void Update()
        {
            EmployeeRepository repository = new EmployeeRepository();
            Employee data = null;
            string document = "0";

            do
            {
                Console.WriteLine("Insira o Documento do Funcionário cadastrado ou 0 para sair:");
                document = Console.ReadLine();

                data = repository.GetByDocument(document);

            } while (data == null && document != "0");

            if (document == "0") return;

            bool differentPass = false;

            do
            {
                Console.WriteLine("Insira a Senha atual:");
                differentPass = Verify(Console.ReadLine(), data.Password);

            } while (!differentPass);


            Console.WriteLine("Insira uma nova senha:");
            string newPass = Console.ReadLine();

            bool confirmation = false;
            do
            {
                Console.WriteLine("Insira a senha novamente:");
                string passwordConfirmation = Console.ReadLine();

                confirmation = newPass.Equals(passwordConfirmation);

            } while (!confirmation);

            bool result = repository.ChangePassword(newPass, data);

            if (result)
                Console.WriteLine("Senha atualizada com Sucesso!");
            else
                Console.WriteLine("Falha ao atualizar a senha.");

            Console.ReadKey();
        }

        public static void Deactivate() {
            Console.WriteLine("Insira o Documento sem números:");
            string document = Console.ReadLine();

            EmployeeService service = new EmployeeService();
            bool result = service.deactivateEMployee(document);

            if (result)
                Console.WriteLine("Funcionário desativado com Sucesso!");
            else
                Console.WriteLine("Falha ao desativar o funcionário.");

            Console.ReadKey();
        }
    }
}
