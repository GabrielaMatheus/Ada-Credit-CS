using AdaCredit.Entities;
using AdaCredit.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit
{
    public class Login
    {
        private readonly EmployeeRepository employeeRepository;

        public Login(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public  void Show()
        {
            bool logginIn = false;

            do
            {
                 Console.Clear();

                Console.WriteLine("Insira o Nome de Usuário:");
                string username = Console.ReadLine();

                Console.WriteLine("Insira a Senha de Usuário:");
                string password = Console.ReadLine();

                logginIn = employeeRepository.VerifyLogin(username, password);

                if (logginIn)
                {
                    employeeRepository.UpdateLastLogin(username);
                }

            } while (!logginIn);

            Console.Clear();
            Console.WriteLine("Login Efetuado com Sucesso!");
            Console.WriteLine("<pressione qualquer tecla para continuar>");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
