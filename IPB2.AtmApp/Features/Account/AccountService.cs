using System;
using System.Collections.Generic;
using System.Linq;

namespace IPB2.AtmApp.Features.Account
{
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
    }
}
