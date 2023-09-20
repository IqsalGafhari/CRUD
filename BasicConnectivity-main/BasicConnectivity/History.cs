using System.Data.SqlClient;
using System.Xml.Linq;

namespace BasicConnectivity;

public class History
{
    public DateTime Start_date { get; set; }
    public int Employee_id { get; set; }
    public DateTime End_date { get; set; }
    public int Department_id { get; set; }
    public string Job_id { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: History
    public List<History> GetAll()
    {
        var histories = new List<History>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM histories";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    histories.Add(new History
                    {
                        Start_date = reader.GetDateTime(0),
                        Employee_id = reader.GetInt32(1),
                        End_date = reader.GetDateTime(2),
                        Department_id = reader.GetInt32(3),
                        Job_id = reader.GetString(4)
                    });
                }
                reader.Close();
                connection.Close();

                return histories;
            }
            reader.Close();
            connection.Close();

            return new List<History>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<History>();
    }

    // GET BY ID: History
    public History GetById(int id)
    {

        var history = new History();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM histories WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek region dengan menggunakan while loop
                    history.Start_date = reader.GetDateTime(0);
                    history.Employee_id = reader.GetInt32(1);
                    history.End_date = reader.GetDateTime(1);
                    history.Department_id = reader.GetInt32(1);
                    history.Job_id = reader.GetString(1);

                }
                reader.Close();
                connection.Close();

                return new History();
            }
            reader.Close();
            connection.Close();

            return new History();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new History();
    }

    // INSERT: History
    public string Insert(DateTime start_date, int employee_id, DateTime end_date, int department_id, string job_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO histories VALUES (@start_date, @employee_id, @end_date, @department_id, @job_id);";

        try
        {
            command.Parameters.Add(new SqlParameter("@start_date", start_date));
            command.Parameters.Add(new SqlParameter("@employee_id", employee_id));
            command.Parameters.Add(new SqlParameter("@end_date", end_date));
            command.Parameters.Add(new SqlParameter("@department_id", department_id));
            command.Parameters.Add(new SqlParameter("@job_id", job_id));

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // UPDATE: History
    public string Update(DateTime start_date, int employee_id, DateTime end_date, int department_id, string job_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE histories SET @start_date = start_date, @employee_id = employee_id, @end_date = end_date, @department_id = department_id, @job_id = job_id WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@start_date", start_date));
            command.Parameters.Add(new SqlParameter("@employee_id", employee_id));
            command.Parameters.Add(new SqlParameter("@end_date", end_date));
            command.Parameters.Add(new SqlParameter("@department_id", department_id));
            command.Parameters.Add(new SqlParameter("@job_id", job_id));
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }

        // return "";
    }

    // DELETE: History
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM histories WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }

        // return "";
    }
}
