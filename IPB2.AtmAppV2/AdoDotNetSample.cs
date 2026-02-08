using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace IPB2.AtmAppV2;

public class AdoDotNetSample
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
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        string query = @"SELECT AccountId, Name, MobileNo, Password, Balance
                         FROM Tbl_Account
                         WHERE IsDelete = 0";

        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine(dr["AccountId"]);
            Console.WriteLine(dr["Name"]);
            Console.WriteLine(dr["MobileNo"]);
            Console.WriteLine(dr["Password"]);
            Console.WriteLine(dr["Balance"]);
            Console.WriteLine("----------------------");
        }
    }

    private void Create()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        Console.Write("Enter name: ");
        string name = Console.ReadLine()!;

        Console.Write("Enter mobile no: ");
        string mobileNo = Console.ReadLine()!;

        Console.Write("Enter password: ");
        string password = Console.ReadLine()!;

        string query = $@"
            INSERT INTO Tbl_Account
            (AccountId, Name, MobileNo, Password, Balance, IsDelete)
            VALUES
            ('{Guid.NewGuid()}',
             '{name}',
             '{mobileNo}',
             '{password}',
             0,
             0)";

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Account created successfully"
            : "Failed to create account");
    }

    private void Update()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

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

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Account updated successfully"
            : "Failed to update account");
    }

    private void Delete()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        Console.Write("Enter account id to delete: ");
        string id = Console.ReadLine()!;

        string query = $@"
            UPDATE Tbl_Account
            SET IsDelete = 1
            WHERE AccountId = '{id}'";

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Account deleted successfully"
            : "Failed to delete account");
    }
}
