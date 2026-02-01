using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.AtmApp.Features.Account
{
    public class AccountUI
    {
        public void CreateAccount()
        {
            Console.WriteLine("=== Create New Account ===");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter your mobile no: ");
            string mobileNo = Console.ReadLine()!;
            EnterPassword:
            Console.Write("Enter your password: ");
            string password = Console.ReadLine()!;
            Console.Write("Enter your confirm password: ");
            string confirmPassword = Console.ReadLine()!;
            if (password != confirmPassword)
            {
                Console.WriteLine("Password and Confirm Password do not match.");
                goto EnterPassword;
            }

            CreateAccountRequestDto accountDto = new CreateAccountRequestDto(
                name, mobileNo, password, confirmPassword);
            AccountService accountService = new AccountService();
            var result = accountService.CreateAccount(accountDto);
            Console.WriteLine(result.Message);
        }
    }

    public class CreateAccountRequestDto
    {
        public CreateAccountRequestDto(string name, string mobileNo, string password, string confirmPassword)
        {
            Name = name;
            MobileNo = mobileNo;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateAccountResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    // Table
    public class AccountDto
    {
        public AccountDto(string id, string name, string mobileNo, string password, decimal balance = 0)
        {
            AccountId = id;
            Name = name;
            MobileNo = mobileNo;
            Password = password;
            Balance = balance;
        }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }
}
