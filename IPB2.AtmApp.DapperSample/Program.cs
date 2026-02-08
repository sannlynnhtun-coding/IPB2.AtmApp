using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "InPersonBatch2";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
{
    db.Open();


    // Ctrl + .
    //var lst = db.Query<AccountDto>("select * from tbl_account").ToList();
    string query2 = "select * from tbl_account";
    List<AccountDto> lst = db.Query<AccountDto>(query2).ToList();

    foreach (AccountDto item in lst)
    {
        Console.WriteLine(item.AccountId);
        Console.WriteLine(item.Name);
        Console.WriteLine(item.Balance);
    }

    Console.Write("Enter name: ");
    string name = Console.ReadLine()!;

    Console.Write("Enter mobile no: ");
    string mobileNo = Console.ReadLine()!;

    Console.Write("Enter password: ");
    string password = Console.ReadLine()!;

    string query = $@"INSERT INTO [dbo].[Tbl_Account]
               ([AccountId]
               ,[Name]
               ,[MobileNo]
               ,[Password]
               ,[Balance]
               ,[IsDelete])
         VALUES
               ('{Guid.NewGuid().ToString()}'
               ,'{name}'
               ,'{mobileNo}'
               ,'{password}'
               ,0
               ,0)";

    int result = db.Execute(query);
    string message = result > 0 ? "Account created successfully" : "Failed to create account";
    Console.WriteLine(message);
}

Console.ReadLine();

public class AccountDto
{
    public string AccountId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string MobileNo { get; set; } = null!;
    public string Password { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsDelete { get; set; }
}