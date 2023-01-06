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
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        
        public Employee getClientByDocument(string document)
        {
            return employeeRepository.GetByDocument(document);
        }

        public bool addEMployee(Employee employee)
        {
            return employeeRepository.Add(employee);
        }

        public bool updateEMployee(string password, Employee employee)
        {
            return employeeRepository.ChangePassword(password, employee);
        }

        public bool deactivateEMployee(string document)
        {
            return employeeRepository.Deactivate(document);
        }

        public Employee employeeActivate()
        {
            return employeeRepository.Activate();
        }
    }
}
