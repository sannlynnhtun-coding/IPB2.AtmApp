using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace IPB2.StaffApp;

public class StaffSample
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

        string query = @"SELECT StaffId, StaffCode, StaffName, Department, Position,
                                Salary, PhoneNo, Email, JoinDate
                         FROM Tbl_Staff
                         WHERE IsDelete = 0";

        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine(dr["StaffId"]);
            Console.WriteLine(dr["StaffCode"]);
            Console.WriteLine(dr["StaffName"]);
            Console.WriteLine(dr["Department"]);
            Console.WriteLine(dr["Position"]);
            Console.WriteLine(dr["Salary"]);
            Console.WriteLine(dr["PhoneNo"]);
            Console.WriteLine(dr["Email"]);
            Console.WriteLine(dr["JoinDate"]);
            Console.WriteLine("----------------------");
        }
    }

    private void Create()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        Console.Write("Enter staff code: ");
        string code = Console.ReadLine()!;

        Console.Write("Enter staff name: ");
        string name = Console.ReadLine()!;

        Console.Write("Enter department: ");
        string department = Console.ReadLine()!;

        Console.Write("Enter position: ");
        string position = Console.ReadLine()!;

        Console.Write("Enter salary: ");
        string salary = Console.ReadLine()!;

        Console.Write("Enter phone no: ");
        string phone = Console.ReadLine()!;

        Console.Write("Enter email: ");
        string email = Console.ReadLine()!;

        string query = $@"
            INSERT INTO Tbl_Staff
            (StaffId, StaffCode, StaffName, Department, Position, Salary, PhoneNo, Email, JoinDate, IsDelete)
            VALUES
            ('{Guid.NewGuid()}',
             '{code}',
             '{name}',
             '{department}',
             '{position}',
             '{salary}',
             '{phone}',
             '{email}',
             GETDATE(),
             0)";

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Staff created successfully"
            : "Failed to create staff");
    }

    private void Update()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        Console.Write("Enter staff id: ");
        string id = Console.ReadLine()!;

        Console.Write("Enter new staff code: ");
        string code = Console.ReadLine()!;

        Console.Write("Enter new staff name: ");
        string name = Console.ReadLine()!;

        Console.Write("Enter new department: ");
        string department = Console.ReadLine()!;

        Console.Write("Enter new position: ");
        string position = Console.ReadLine()!;

        Console.Write("Enter new salary: ");
        string salary = Console.ReadLine()!;

        Console.Write("Enter new phone no: ");
        string phone = Console.ReadLine()!;

        Console.Write("Enter new email: ");
        string email = Console.ReadLine()!;

        string query = $@"
            UPDATE Tbl_Staff
            SET StaffCode = '{code}',
                StaffName = '{name}',
                Department = '{department}',
                Position = '{position}',
                Salary = '{salary}',
                PhoneNo = '{phone}',
                Email = '{email}'
            WHERE StaffId = '{id}'
            AND IsDelete = 0";

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Staff updated successfully"
            : "Failed to update staff");
    }

    private void Delete()
    {
        using SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        Console.Write("Enter staff id to delete: ");
        string id = Console.ReadLine()!;

        string query = $@"
            UPDATE Tbl_Staff
            SET IsDelete = 1
            WHERE StaffId = '{id}'";

        SqlCommand cmd = new SqlCommand(query, connection);

        int result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0
            ? "Staff deleted successfully"
            : "Failed to delete staff");
    }
}
