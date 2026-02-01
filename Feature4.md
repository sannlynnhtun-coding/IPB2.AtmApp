Now we add the feature to take money out from an account.

---

### 1) What this feature does

When user chooses Withdraw:

1. Ask for **Mobile No**
2. Ask for **Amount**
3. Validate:
    - account must exist
    - amount must be greater than 0
    - amount must be **less than or equal to** current balance
4. Subtract amount from `Balance`
5. Return a message to UI

---

### 2) What we need to add

### A) Withdraw Request DTO

```csharp
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

```

### B) Add Withdraw method inside `AccountService`

Add this inside `AccountService`:

```csharp
public BasicResponseDto Withdraw(WithdrawRequestDto request)
{
    if (string.IsNullOrWhiteSpace(request.MobileNo))
        return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

    if (request.Amount <= 0)
        return new BasicResponseDto { IsSuccess = false, Message = "Amount must be greater than 0." };

    var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
    if (account is null)
        return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

    if (request.Amount > account.Balance)
        return new BasicResponseDto { IsSuccess = false, Message = "Insufficient balance." };

    account.Balance -= request.Amount;

    return new BasicResponseDto
    {
        IsSuccess = true,
        Message = $"Withdraw successful. Current balance: {account.Balance}"
    };
}

```

---

### 3) Update UI menu to test Withdraw

Update `Start()` menu:

```csharp
Console.WriteLine("\n=== ATM App (Step 4) ===");
Console.WriteLine("1) Create Account");
Console.WriteLine("2) Deposit");
Console.WriteLine("3) Check Balance");
Console.WriteLine("4) Withdraw");
Console.WriteLine("5) Exit");

```

Update choice handling:

```csharp
if (choice == "1") CreateAccount();
else if (choice == "2") Deposit();
else if (choice == "3") CheckBalance();
else if (choice == "4") Withdraw();
else if (choice == "5") return;
else Console.WriteLine("Invalid option.");

```

### Add `Withdraw()` method in `AccountUI`

```csharp
private void Withdraw()
{
    Console.WriteLine("\n=== Withdraw ===");

    Console.Write("Enter your mobile no: ");
    string mobileNo = Console.ReadLine() ?? "";

    Console.Write("Enter amount: ");
    if (!decimal.TryParse(Console.ReadLine(), out var amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    var req = new WithdrawRequestDto(mobileNo, amount);
    var result = _service.Withdraw(req);

    Console.WriteLine(result.Message);
}

```

---

### 4) How to test Feature 4

1. Run program
2. Create account with mobile `0912345678`
3. Deposit `5000`
4. Withdraw `2000` → should succeed and balance becomes `3000`
5. Withdraw `5000` → should fail with “Insufficient balance.”
6. Withdraw `1` or `0` → should fail with “Amount must be greater than 0.”
7. Unknown mobile → “Account not found.”

---