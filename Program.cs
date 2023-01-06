// See https://aka.ms/new-console-template for more information
using AdaCredit;
using AdaCredit.controllers;
using AdaCredit.Entities;
using AdaCredit.enums;
using AdaCredit.repositories;
using AdaCredit.services;
using Bogus;
using Bogus.DataSets;
using CsvHelper;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Xml;
using System.Xml.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);
        EmployeeRepository employeeRepository = new EmployeeRepository();
        ClientRepository.LoadClients();
        //TransactionRepository.ViewFiles();

        GenerateData.Populate();
        TransactionsController.Execute();

        Login loginScreen = new Login(employeeRepository);
        loginScreen.Show();

        Menu menuScren = new Menu();
        menuScren.Show();
    }
}

