// See https://aka.ms/new-console-template for more information
using IPB2.AtmApp.Features.Account;

Console.WriteLine("ATM");

Start:
AccountUI accountUI = new AccountUI();  
accountUI.CreateAccount();
goto Start;

Console.ReadLine();