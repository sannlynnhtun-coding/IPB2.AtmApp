Now we finish the flow by:

- adding **Exit** behavior in both menus
- showing the **final clean menu structure** that can call all features

---

## 1) Exit behavior (how it works)

### Exit from Main menu

- User chooses Exit → app loop ends (`return;`)

### Exit from Session menu

- User chooses Exit → close the whole program
(easy way: `Environment.Exit(0)`)

---

## 2) Final menu design (two menus)

### A) Main menu (no active session)

Purpose: create account, enter account, exit

### B) Session menu (active session)

Purpose: actions for the current account, logout, exit

---

## 3) Final UI code (menus + calling all features)

> This code assumes you already have:
> 
- `AccountService`
- DTOs: `CreateAccountRequestDto`, `DepositRequestDto`, `WithdrawRequestDto`, `BalanceRequestDto`
- Response DTOs: `CreateAccountResponseDto`, `BasicResponseDto`
- Model: `AccountDto`
- Service methods: `CreateAccount`, `EnterAccount`, `Deposit`, `Withdraw`, `GetBalance`

```csharp
using System;

public class AccountUI
{
    private readonly AccountService _service = new AccountService();
    private string? _currentMobileNo;

    public void Start()
    {
        while (true)
        {
            ShowMainMenu();
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\\n=== Main Menu ===");
        Console.WriteLine("1) Create Account");
        Console.WriteLine("2) Enter Account");
        Console.WriteLine("3) Exit");
        Console.Write("Choose: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateAccount();
                break;

            case "2":
                EnterAccount();
                break;

            case "3":
                return; // Exit program
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    private void SessionMenu()
    {
        while (_currentMobileNo != null)
        {
            Console.WriteLine("\\n=== Session Menu ===");
            Console.WriteLine("1) Deposit");
            Console.WriteLine("2) Check Balance");
            Console.WriteLine("3) Withdraw");
            Console.WriteLine("4) Logout");
            Console.WriteLine("5) Exit");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Deposit();
                    break;

                case "2":
                    CheckBalance();
                    break;

                case "3":
                    Withdraw();
                    break;

                case "4":
                    Logout();
                    return; // back to main menu

                case "5":
                    Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void CreateAccount()
    {
        Console.WriteLine("\\n=== Create New Account ===");

        Console.Write("Enter your name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter your mobile no: ");
        string mobileNo = Console.ReadLine() ?? "";

        string password;
        string confirmPassword;

        while (true)
        {
            Console.Write("Enter your password: ");
            password = Console.ReadLine() ?? "";

            Console.Write("Enter your confirm password: ");
            confirmPassword = Console.ReadLine() ?? "";

            if (password == confirmPassword) break;

            Console.WriteLine("Password and Confirm Password do not match.");
        }

        var req = new CreateAccountRequestDto(name, mobileNo, password, confirmPassword);
        var result = _service.CreateAccount(req);

        Console.WriteLine(result.Message);
    }

    private void EnterAccount()
    {
        Console.WriteLine("\\n=== Enter Account ===");
        Console.Write("Enter your mobile no: ");
        string mobileNo = Console.ReadLine() ?? "";

        var result = _service.EnterAccount(mobileNo);
        Console.WriteLine(result.Message);

        if (!result.IsSuccess) return;

        _currentMobileNo = mobileNo.Trim();
        SessionMenu();
    }

    private void Deposit()
    {
        Console.WriteLine("\\n=== Deposit ===");
        Console.Write("Enter amount: ");

        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        var result = _service.Deposit(new DepositRequestDto(_currentMobileNo!, amount));
        Console.WriteLine(result.Message);
    }

    private void CheckBalance()
    {
        Console.WriteLine("\\n=== Check Balance ===");
        var result = _service.GetBalance(new BalanceRequestDto(_currentMobileNo!));
        Console.WriteLine(result.Message);
    }

    private void Withdraw()
    {
        Console.WriteLine("\\n=== Withdraw ===");
        Console.Write("Enter amount: ");

        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        var result = _service.Withdraw(new WithdrawRequestDto(_currentMobileNo!, amount));
        Console.WriteLine(result.Message);
    }

    private void Logout()
    {
        _currentMobileNo = null;
        Console.WriteLine("Logged out.");
    }
}

```

And `Program` to run:

```csharp
public class Program
{
    public static void Main()
    {
        new AccountUI().Start();
    }
}

```

---

## 4) Quick test plan (all features)

1. Create Account (mobile: 0912345678)
2. Enter Account (0912345678)
3. Deposit 5000
4. Check Balance → 5000
5. Withdraw 2000
6. Check Balance → 3000
7. Logout → back to main menu
8. Exit

---