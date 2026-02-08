using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.AtmApp.DapperSample;

public class DapperSample
{
    private readonly SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "InPersonBatch2",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    public void Run()
    {
        Read();
        Create();
        Update();
        Delete();
    }

    private void Read()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();

            string query2 = "select * from tbl_account";
            List<AccountDto> lst = db.Query<AccountDto>(query2).ToList();

            foreach (AccountDto item in lst)
            {
                Console.WriteLine(item.AccountId);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Balance);
            }
        }
    }

    private void Create()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
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
    }

    private void Update()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            Console.Write("Enter account id: ");
            string id = Console.ReadLine()!;

            Console.Write("Enter new name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter new mobile no: ");
            string mobileNo = Console.ReadLine()!;

            Console.Write("Enter new password: ");
            string password = Console.ReadLine()!;

            Console.Write("Enter new balance: ");
            string balance = Console.ReadLine()!;

            string query = $@"
            UPDATE Tbl_Account
            SET Name = '{name}',
                MobileNo = '{mobileNo}',
                Password = '{password}',
                Balance = '{balance}'
            WHERE AccountId = '{id}'
            AND IsDelete = 0";

            int result = db.Execute(query);

            string message = result > 0
                ? "Account updated successfully"
                : "Failed to update account";

            Console.WriteLine(message);
        }
    }

    private void Delete()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            Console.Write("Enter account id to delete: ");
            string id = Console.ReadLine()!;

            string query = $@"
            UPDATE Tbl_Account
            SET IsDelete = 1
            WHERE AccountId = '{id}'";

            int result = db.Execute(query);

            string message = result > 0
                ? "Account deleted successfully"
                : "Failed to delete account";

            Console.WriteLine(message);
        }
    }
}
