Now we add the next feature so a user can put money into an account.

---

### 1) What this feature does

When user chooses Deposit:

1. Ask for **Mobile No** (to find the account)
2. Ask for **Amount**
3. Validate:
    - account must exist
    - amount must be greater than 0
4. Add amount to `Balance`
5. Return a message to UI

> We’re still keeping it simple: no login yet. We identify account by mobile number.
> 

---

### 2) What we need to add

### A) Deposit Request DTO

```csharp
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

```

### B) Basic Response DTO (reusable)

```csharp
public class BasicResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}

```

### C) Add Deposit method inside `AccountService`

Add this inside `AccountService`:

```csharp
public BasicResponseDto Deposit(DepositRequestDto request)
{
    if (string.IsNullOrWhiteSpace(request.MobileNo))
        return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

    if (request.Amount <= 0)
        return new BasicResponseDto { IsSuccess = false, Message = "Amount must be greater than 0." };

    var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
    if (account is null)
        return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

    account.Balance += request.Amount;

    return new BasicResponseDto
    {
        IsSuccess = true,
        Message = $"Deposit successful. Current balance: {account.Balance}"
    };
}

```

---

### 3) Update UI menu to test Deposit

Update `Start()` menu:

```csharp
Console.WriteLine("\n=== ATM App (Step 2) ===");
Console.WriteLine("1) Create Account");
Console.WriteLine("2) Deposit");
Console.WriteLine("3) Exit");

```

Update choice handling:

```csharp
if (choice == "1") CreateAccount();
else if (choice == "2") Deposit();
else if (choice == "3") return;
else Console.WriteLine("Invalid option.");

```

### Add `Deposit()` method in `AccountUI`

```csharp
private void Deposit()
{
    Console.WriteLine("\n=== Deposit ===");

    Console.Write("Enter your mobile no: ");
    string mobileNo = Console.ReadLine() ?? "";

    Console.Write("Enter amount: ");
    if (!decimal.TryParse(Console.ReadLine(), out var amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    var req = new DepositRequestDto(mobileNo, amount);
    var result = _service.Deposit(req);

    Console.WriteLine(result.Message);
}

```

---

### 4) How to test Feature 2

1. Run program
2. Create account with mobile `0912345678`
3. Choose Deposit
4. Enter same mobile `0912345678`
5. Enter amount `5000`
6. Should show: `Deposit successful. Current balance: 5000`
7. Try deposit with unknown mobile → “Account not found.”
8. Try amount `0` or negative → “Amount must be greater than 0.”

---