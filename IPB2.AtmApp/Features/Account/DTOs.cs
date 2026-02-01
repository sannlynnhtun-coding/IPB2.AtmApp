
namespace IPB2.AtmApp.Features.Account
{
    // C) Response DTO (output to UI)
    public class CreateAccountResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    // A) Data model (stored record)
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

    // B) Request DTO (input from UI)
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

    public class LoginRequestDto
    {
        public LoginRequestDto(string mobileNo, string password)
        {
            MobileNo = mobileNo;
            Password = password;
        }

        public string MobileNo { get; set; }
        public string Password { get; set; }
    }

    public class DepositRequestDto
    {
        public DepositRequestDto(string mobileNo, decimal amount)
        {
            MobileNo = mobileNo;
            Amount = amount;
        }

        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
    }

    public class BasicResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class BalanceRequestDto
    {
        public BalanceRequestDto(string mobileNo)
        {
            MobileNo = mobileNo;
        }

        public string MobileNo { get; set; }
    }

    public class WithdrawRequestDto
    {
        public WithdrawRequestDto(string mobileNo, decimal amount)
        {
            MobileNo = mobileNo;
            Amount = amount;
        }

        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
    }
}
