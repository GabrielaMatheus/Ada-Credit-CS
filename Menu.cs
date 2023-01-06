using AdaCredit.controllers;
using AdaCredit.Entities;
using AdaCredit.repositories;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.Entities;

namespace AdaCredit
{
    public class Menu
    {
        public void Show()
        {
            ConsoleMenu subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
              .Add("Cadastrar um Novo Cliente", ClientController.Add)
              .Add("Consultar Dados de um Cliente pelo CPF", ClientController.FetchByDocument)
              .Add("Alterar Cadastro de um Cliente", ClientController.Update)
              .Add("Desativar Cadastro de um Cliente", ClientController.Deactivate)
              .Add("Voltar", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Ada Credit / Submenu";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                  config.SelectedItemBackgroundColor = ConsoleColor.Blue;

              });

            ConsoleMenu subEmployee = new ConsoleMenu(Array.Empty<string>(), level: 2)
              .Add("Cadastrar um Novo Funcionário", EmployeeController.Add)
              .Add("Alterar senha de um Funcionário", EmployeeController.Update)
              .Add("Desativar Cadastro de um Funcionário", EmployeeController.Deactivate)
              .Add("Voltar", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Ada Credit - Submenu";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                  config.SelectedItemBackgroundColor = ConsoleColor.Blue;
              });

            ConsoleMenu subReports = new ConsoleMenu(Array.Empty<string>(), level: 3)
              .Add("Exibir Todos os Clientes Ativos com seus Saldos", ReportController.ShowAllClientsActivatedAndAmount)
              .Add("Exibir Todos os Clientes Inativos", ReportController.ShowAllClientsDesactivated)
              .Add("Exibir Todos os Funcionários Ativos e sua Última Data e Hora de Login", ReportController.ShowAllEmployeesActivatedAndLastLogin)
              .Add("Exibir Transações com Erro", ReportController.ShowTransactionsWithError)
              .Add("Voltar", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Ada Credit - Submenu";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                  config.SelectedItemBackgroundColor = ConsoleColor.Blue;
              });

            ConsoleMenu subTransactions = new ConsoleMenu(Array.Empty<string>(), level: 3)
            .Add("Processar Transações", TransactionsController.Execute)
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Ada Credit - Submenu";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                config.SelectedItemBackgroundColor = ConsoleColor.Blue;
            });

            ConsoleMenu menu = new ConsoleMenu(Array.Empty<string>(), level: 0)
              .Add("Clientes", () => subClient.Show())
              .Add("Funcionários", () => subEmployee.Show())
              .Add("Relatórios", () => subReports.Show())
              .Add("Transações", () => subTransactions.Show())
              .Add("Sair", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Ada Credit - Menu Principal";
                  config.EnableWriteTitle = false;
                  config.EnableBreadcrumb = true;
                  config.SelectedItemBackgroundColor = ConsoleColor.Blue;
              });

            menu.Show();
        }
    }
}
