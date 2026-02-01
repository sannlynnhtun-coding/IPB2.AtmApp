Now we add the feature to view the current balance.

---

### 1) What this feature does

When user chooses Check Balance:

1. Ask for **Mobile No**
2. Find the account in the fake database
3. If account not found → show error message
4. If found → show the current `Balance`

---

### 2) What we need to add

### A) Balance Request DTO

```csharp
public class BalanceRequestDto
{
    public BalanceRequestDto(string mobileNo)
    {
        MobileNo = mobileNo;
    }

    public string MobileNo { get; set; }
}

```

### B) Add Balance method inside `AccountService`

Add this inside `AccountService`:

```csharp
public BasicResponseDto GetBalance(BalanceRequestDto request)
{
    if (string.IsNullOrWhiteSpace(request.MobileNo))
        return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

    var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
    if (account is null)
        return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

    return new BasicResponseDto
    {
        IsSuccess = true,
        Message = $"Current balance: {account.Balance}"
    };
}

```

---

### 3) Update UI menu to test Check Balance

Update `Start()` menu:

```csharp
Console.WriteLine("\n=== ATM App (Step 3) ===");
Console.WriteLine("1) Create Account");
Console.WriteLine("2) Deposit");
Console.WriteLine("3) Check Balance");
Console.WriteLine("4) Exit");

```

Update choice handling:

```csharp
if (choice == "1") CreateAccount();
else if (choice == "2") Deposit();
else if (choice == "3") CheckBalance();
else if (choice == "4") return;
else Console.WriteLine("Invalid option.");

```

### Add `CheckBalance()` method in `AccountUI`

```csharp
private void CheckBalance()
{
    Console.WriteLine("\n=== Check Balance ===");

    Console.Write("Enter your mobile no: ");
    string mobileNo = Console.ReadLine() ?? "";

    var req = new BalanceRequestDto(mobileNo);
    var result = _service.GetBalance(req);

    Console.WriteLine(result.Message);
}

```

---

### 4) How to test Feature 3

1. Run program
2. Create account with mobile `0912345678`
3. Deposit `5000`
4. Choose Check Balance
5. Enter `0912345678`
6. Should show: `Current balance: 5000`
7. Try unknown mobile → “Account not found.”

---