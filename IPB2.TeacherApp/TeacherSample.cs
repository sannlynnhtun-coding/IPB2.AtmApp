using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IPB2.TeacherApp;

public class DapperTeacherSample
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

            string query = "SELECT * FROM Teacher";
            List<TeacherDto> teachers = db.Query<TeacherDto>(query).ToList();

            foreach (var teacher in teachers)
            {
                Console.WriteLine($"ID: {teacher.TeacherID}, Name: {teacher.Name}, Phone: {teacher.Phone}");
            }
        }
    }

    private void Create()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter phone: ");
            string phone = Console.ReadLine()!;

            string query = @"INSERT INTO Teacher (Name, Phone)
                                 VALUES (@Name, @Phone)";

            int result = db.Execute(query, new { Name = name, Phone = phone });

            Console.WriteLine(result > 0
                ? "Teacher created successfully"
                : "Failed to create teacher");
        }
    }

    private void Update()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            Console.Write("Enter Teacher ID to update: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter new name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter new phone: ");
            string phone = Console.ReadLine()!;

            string query = @"UPDATE Teacher
                                 SET Name = @Name,
                                     Phone = @Phone
                                 WHERE TeacherID = @TeacherID";

            int result = db.Execute(query, new { Name = name, Phone = phone, TeacherID = id });

            Console.WriteLine(result > 0
                ? "Teacher updated successfully"
                : "Failed to update teacher");
        }
    }

    private void Delete()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            Console.Write("Enter Teacher ID to delete: ");
            int id = int.Parse(Console.ReadLine()!);

            string query = @"DELETE FROM Teacher WHERE TeacherID = @TeacherID";

            int result = db.Execute(query, new { TeacherID = id });

            Console.WriteLine(result > 0
                ? "Teacher deleted successfully"
                : "Failed to delete teacher");
        }
    }
}

// DTO class for Teacher
public class TeacherDto
{
    public int TeacherID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
}
