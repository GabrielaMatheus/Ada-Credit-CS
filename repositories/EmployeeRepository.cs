using AdaCredit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Bogus.DataSets;
using AdaCredit.controllers;
using static AdaCredit.enums.Status;
using CsvHelper;
using System.Globalization;

namespace AdaCredit.repositories
{
    public class EmployeeRepository
    {

        private static List<Employee> employees = new List<Employee>()
        {
        new Employee("", "", "user", HashPassword("pass", GenerateSalt(12)))
        };
        public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private void Save()
        {
            string filePath = Path.Combine(desktopPath, "employees.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(employees);
                writer.Flush();
            }
        }

        public bool Add(Employee employee)
        {
            if (employees.Count == 0) return false;

            if (employees.Any(x => x.Document.Equals(employee.Document)))
            {
                Console.WriteLine("Funcionário já cadastrado.");
                Console.ReadKey();

                return false;
            }

            Employee entity2 = new Employee(employee.Name, employee.Document, employee.Username, employee.Password);
            employees.Add(entity2);

            Save();

            return true;
        }

        public bool ChangePassword(string password,Employee employee)
        {
            if (employee == null) return false;

            employee.changePassword(HashPassword(password, GenerateSalt(12)));
            return true;
        }

        public void UpdateLastLogin(string employeeUsername)
        {
            Employee employee = GetByUsername(employeeUsername);
            employee.changeLastLogin();
        }
        public Employee GetByUsername(string username)
        {
            Employee employeee = new Employee();

            if (employees.Count == 0) return null;

            foreach (Employee employee in employees)
            {
                if (employee.Username.Equals(username))
                {
                    return employee;
                }
                else
                {
                    Console.WriteLine("Usuário não encontrado.");
                    break;
                }
            }
            return null;
        }

        public Employee GetByDocument(string document)
        {
            Employee employeee = new Employee();

            if (employees.Count == 0) return null;

            foreach (Employee employee in employees)
            {

                if (employee.Document.Equals(document))
                {
                    return employee;
                }
                else
                {
                    Console.WriteLine("Funcionário não encontrado.");
                    break;
                }
            }
            return null;
        }


        public bool VerifyLogin(string username, string password)
        {
            if (employees.Count == 0) return false;

            bool succesfullLogin = false;

            foreach (Employee user in employees)
            {
                if (!user.Username.Equals(username)) return false;

                succesfullLogin = Verify(password, user.Password);

                if (succesfullLogin)
                {
                    if (verifyFirstLogin()) return false;
                    return true;
                }
            }
            return false;
        }

        public bool verifyFirstLogin()
        {
            
            if ("user".Equals(employees[0].Username))
            {
                Console.WriteLine("PRIMEIRO ACESSO, CADASTRE UM FUNCIONÁRIO");
                EmployeeController.Add();
                employees.RemoveAt(0);
                return true;
            }
            return false;
        }

        public bool Deactivate(string document)
        {
            Employee employee = GetByDocument(document);

            if (employee == null) return false;

            employee.status = status.Desativado;
            Save();
            return true;
        }

        public Employee Activate()
        {
            if (employees.Count == 0) return null;

            foreach (Employee employee in employees)
            {
                if (employee.status.Equals(status.Ativado))
                {
                    Console.WriteLine(employee);
                    continue;
                }
            }
            return null;
        }
    }
}
