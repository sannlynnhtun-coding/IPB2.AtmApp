Now we add the idea of a **session** so a user must “enter” an account first, then do actions, then logout.

Until now we always asked mobile no for every action. From this point:

- User “enters” an account once (by mobile)
- UI stores the current account (like `_currentMobileNo`)
- Actions use that stored value
- Logout clears it

---

### 1) What this feature does

1. User selects “Enter Account”
2. UI asks for **Mobile No**
3. Service checks if account exists
4. If exists → UI stores it as “current account”
5. User can do actions without typing mobile every time
6. Logout clears current account and returns to main menu

---

### 2) What we need to add

### A) Login Request DTO

```csharp
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
```

### B) A small “enter account” check in Service

We’ll reuse `BasicResponseDto`.

Add this method inside `AccountService`:

```csharp
public BasicResponseDto Login(LoginRequestDto request)
{
    if (string.IsNullOrWhiteSpace(request.MobileNo))
        return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

    if (string.IsNullOrWhiteSpace(request.Password))
        return new BasicResponseDto { IsSuccess = false, Message = "Please enter your password." };

    var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
    if (account is null)
        return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };
        
    if (account.Password != request.Password)
        return new BasicResponseDto { IsSuccess = false, Message = "Invalid password." };

    return new BasicResponseDto { IsSuccess = true, Message = $"Hello, {account.Name}!" };
}
```

---

### C) Store current account in UI

Add a field in `AccountUI`:

```csharp
private string? _currentMobileNo;

```

---

### 3) Update UI flow (two menus: Main and Session)

### A) Main menu (not in session)

Main menu shows options like:

- Create Account
- Enter Account
- Exit

When user enters account successfully → go to Session menu.

✅ Add method: `EnterAccount()`

```csharp
private void EnterAccount()
{
    Console.WriteLine("\n=== Enter Account ===");
    Console.Write("Enter your mobile no: ");
    string mobileNo = Console.ReadLine() ?? "";

    Console.Write("Enter your password: ");
    string password = Console.ReadLine() ?? "";

    var result = _service.Login(new LoginRequestDto(mobileNo, password));
    Console.WriteLine(result.Message);

    if (!result.IsSuccess) return;

    _currentMobileNo = mobileNo.Trim();
    SessionMenu();
}

```

---

### B) Session menu (actions for current account)

Now these actions do NOT ask for mobile anymore.

They use `_currentMobileNo`.

✅ Update Deposit method (no mobile input)

```csharp
private void Deposit()
{
    Console.WriteLine("\n=== Deposit ===");
    Console.Write("Enter amount: ");

    if (!decimal.TryParse(Console.ReadLine(), out var amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    var result = _service.Deposit(new DepositRequestDto(_currentMobileNo!, amount));
    Console.WriteLine(result.Message);
}

```

✅ Update Withdraw method (no mobile input)

```csharp
private void Withdraw()
{
    Console.WriteLine("\n=== Withdraw ===");
    Console.Write("Enter amount: ");

    if (!decimal.TryParse(Console.ReadLine(), out var amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    var result = _service.Withdraw(new WithdrawRequestDto(_currentMobileNo!, amount));
    Console.WriteLine(result.Message);
}

```

✅ Update CheckBalance method (no mobile input)

```csharp
private void CheckBalance()
{
    Console.WriteLine("\n=== Check Balance ===");
    var result = _service.GetBalance(new BalanceRequestDto(_currentMobileNo!));
    Console.WriteLine(result.Message);
}

```

✅ Add Logout method

```csharp
private void Logout()
{
    _currentMobileNo = null;
    Console.WriteLine("Logged out.");
}

```

---

### 4) How to test Feature 5

1. Create account
2. Enter account using mobile
3. Deposit / Withdraw / Balance should work without asking mobile
4. Logout
5. After logout, session actions should not be accessible (because you return to main menu)

---