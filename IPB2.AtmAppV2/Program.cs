using Microsoft.Data.SqlClient;
using System.Data;

// ADO.NET - Microsoft.Data.SqlClient
// Dapper
// EFCore

string a = default;
int b = default;

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "InPersonBatch2";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();

// Ctrl + K, C

#region Read

//SqlCommand cmd = new SqlCommand(@"SELECT [AccountId]
//      ,[Name]
//      ,[MobileNo]
//      ,[Password]
//      ,[Balance]
//  FROM [dbo].[Tbl_Account] Where IsDelete = 0", connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt);

#endregion

#region Create

//Console.Write("Enter name: ");
//string name = Console.ReadLine();

//Console.Write("Enter mobile no: ");
//string mobileNo = Console.ReadLine();

//Console.Write("Enter password: ");
//string password = Console.ReadLine();

//string query = $@"INSERT INTO [dbo].[Tbl_Account]
//           ([AccountId]
//           ,[Name]
//           ,[MobileNo]
//           ,[Password]
//           ,[Balance]
//           ,[IsDelete])
//     VALUES
//           ('{Guid.NewGuid().ToString()}'
//           ,'{name}'
//           ,'{mobileNo}'
//           ,'{password}'
//           ,0
//           ,0)";

//SqlCommand cmd = new SqlCommand(query, connection);
//int result = cmd.ExecuteNonQuery();  
//string message = result > 0 ? 
//    "Account created successfully" : 
//    "Failed to create account";
//Console.WriteLine(message);

#endregion

#region Update

//Console.Write("Enter account id: ");
//string id = Console.ReadLine();

//// validation (exist id)

//Console.Write("Enter name: ");
//string name = Console.ReadLine();

//Console.Write("Enter mobile no: ");
//string mobileNo = Console.ReadLine();

//Console.Write("Enter password: ");
//string password = Console.ReadLine();

//Console.Write("Enter balance: ");
//string balance = Console.ReadLine();

//string query = $@"
//UPDATE [dbo].[Tbl_Account]
//   SET [Name] = '{name}'
//      ,[MobileNo] = '{mobileNo}'
//      ,[Password] = '{password}'
//      ,[Balance] = '{balance}'
// WHERE AccountId = '{id}'";

//SqlCommand cmd = new SqlCommand(query, connection);
//int result = cmd.ExecuteNonQuery();
//string message = result > 0 ?
//    "Account updated successfully" :
//    "Failed to update account";
//Console.WriteLine(message);

#endregion

#region Delete

//Console.Write("Enter account id: ");
//string id = Console.ReadLine();

//string query = $@"
//UPDATE [dbo].[Tbl_Account]
//   SET IsDelete = 1
// WHERE AccountId = '{id}'";

//SqlCommand cmd = new SqlCommand(query, connection);
//int result = cmd.ExecuteNonQuery();
//string message = result > 0 ?
//    "Account deleted successfully" :
//    "Failed to delete account";
//Console.WriteLine(message);

#endregion

// CRUD - Create, Read, Update, Delete

connection.Close();

// table
// row
// column

//foreach(DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["AccountId"]);   
//    Console.WriteLine(dr["Name"]);   
//    Console.WriteLine(dr["MobileNo"]);   
//    Console.WriteLine(dr["Password"]);   
//    Console.WriteLine(dr["Balance"]);   
//}

Console.ReadLine();