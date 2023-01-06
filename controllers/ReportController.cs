using AdaCredit.Entities;
using AdaCredit.repositories;
using AdaCredit.services;

namespace AdaCredit.controllers
{
    public class ReportController
    {
      public static void ShowAllClientsActivatedAndAmount(){
        ClientService service = new ClientService();
        Client result = service.clientActivate();

        Console.WriteLine(result);
        Console.ReadKey();
      }

      public static void ShowAllClientsDesactivated(){
        ClientService service = new ClientService();
        Client result =  service.statusDeactivated();

        Console.WriteLine(result);
        Console.ReadKey();
      }

      public static void ShowAllEmployeesActivatedAndLastLogin(){
        EmployeeService service = new EmployeeService();
        Employee result = service.employeeActivate();

        Console.WriteLine(result);
        Console.ReadKey();
      }

      public static void ShowTransactionsWithError(){
        TransactionRepository.ViewFailedFiles();
      }

     
    }
}