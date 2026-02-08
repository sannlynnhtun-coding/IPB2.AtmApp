public class AccountDto
{
    public string AccountId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string MobileNo { get; set; } = null!;
    public string Password { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsDelete { get; set; }
}