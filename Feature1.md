This feature allows a **new user** to create an account in the ATM system.

At the end of this feature:

- User information is stored in memory
- The account has a unique identity
- The balance always starts from **0**

---

### 1) What this feature does

When user selects **Create Account**:

1. Ask for **Name**
2. Ask for **Mobile No**
3. Ask for **Password** + **Confirm Password**
4. If password mismatch → ask again
5. Save account into a **fake database (List)**
6. Show success/fail message

---

### 2) Classes we need (small set)

### A) Data model (stored record)

This is like a “table row”.

```csharp
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

```

### B) Request DTO (input from UI)

```csharp
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

```

### C) Response DTO (output to UI)

```csharp
public class CreateAccountResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}

```

### D) Service (business rules + fake DB)

- Fake DB: `static List<AccountDto> _accounts`
- Rules:
    - mobile must be unique
    - (optional) required field checks

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class AccountService
{
    private static readonly List<AccountDto> _accounts = new();

    public CreateAccountResponseDto CreateAccount(CreateAccountRequestDto request)
    {
        // required checks (recommended)
        if (string.IsNullOrWhiteSpace(request.Name))
            return new CreateAccountResponseDto { IsSuccess = false, Message = "Please enter your name." };

        if (string.IsNullOrWhiteSpace(request.MobileNo))
            return new CreateAccountResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

        if (string.IsNullOrWhiteSpace(request.Password))
            return new CreateAccountResponseDto { IsSuccess = false, Message = "Please enter your password." };

        if (request.Password != request.ConfirmPassword)
            return new CreateAccountResponseDto { IsSuccess = false, Message = "Password and Confirm Password do not match." };

        bool isExistMobileNo = _accounts.Any(x => x.MobileNo == request.MobileNo);
        if (isExistMobileNo)
            return new CreateAccountResponseDto { IsSuccess = false, Message = "Mobile No already exists." };

        var account = new AccountDto(
            Guid.NewGuid().ToString(),
            request.Name.Trim(),
            request.MobileNo.Trim(),
            request.Password,
            0
        );

        _accounts.Add(account);

        return new CreateAccountResponseDto { IsSuccess = true, Message = "Account created successfully." };
    }
}

```

---

### 3) UI for this feature (with a tiny menu to test)

We only add menu items needed to test Create Account now.

```csharp
using System;

public class AccountUI
{
    private readonly AccountService _service = new AccountService();

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\n=== ATM App (Step 1) ===");
            Console.WriteLine("1) Create Account");
            Console.WriteLine("2) Exit");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            if (choice == "1") CreateAccount();
            else if (choice == "2") return;
            else Console.WriteLine("Invalid option.");
        }
    }

    private void CreateAccount()
    {
        Console.WriteLine("\n=== Create New Account ===");

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
}

```

And a `Program` to run it:

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

### 4) How to test Feature 1

1. Run the program
2. Choose `1`
3. Create account with mobile: `0912345678`
4. Try creating again with same mobile → should show “Mobile No already exists.”
5. Try mismatch password → should ask again until correct

---