using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    private static void Main()
    {
        //GetAllRegions();
        InsertRegion("Jawa Utara");
    }

    // GET ALL: Region
    public static void GetAllRegions()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // GET BY ID: Region
    public static void GetRegionById(int id)
    {
    }

    // INSERT: Region
    public static void InsertRegion(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO regions VALUES (@name);";

        try
        {
            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Insert Success");
                        break;
                    default:
                        Console.WriteLine("Insert Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // UPDATE: Region
    public static void UpdateRegion(int id, string name)
    {
        //string connectionString = "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;"; ; // Replace with your actual connection string

        // Define the update query
        string updateQuery = "UPDATE Regions SET id = @id, Name = @Name";

        // Create and configure the SqlConnection and SqlCommand
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(updateQuery, connection))
        {
            // Define parameters
            command.Parameters.AddWithValue("@id", "Newid"); // Replace with the new first name
            command.Parameters.AddWithValue("@Name", "NewName");   // Replace with the new last name
            //command.Parameters.AddWithValue("@StudentID", 1);             // Replace with the specific student ID to update

            try
            {
                connection.Open();

                // Execute the update query
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update successful. Rows affected: " + rowsAffected);
                }
                else
                {
                    Console.WriteLine("No records were updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // DELETE: Region
    public static void DeleteRegion(int id)
    {
        //string connectionString = "Your_Connection_String"; // Replace with your actual connection string

        // Define the delete query
        string deleteQuery = "DELETE FROM Regions WHERE id = @id";

        // Create and configure the SqlConnection and SqlCommand
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
        {
            // Define the parameter for the StudentID
            command.Parameters.AddWithValue("id", 1); // Replace with the specific student ID to delete

            try
            {
                connection.Open();

                // Execute the delete query
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Delete successful. Rows affected: " + rowsAffected);
                }
                else
                {
                    Console.WriteLine("No records were deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}