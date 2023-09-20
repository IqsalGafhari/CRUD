using System.Data.SqlClient;
using System.Xml.Linq;

namespace BasicConnectivity;

public class Job
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int Min_salary { get; set; }
    public int Max_salary { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Job
    public List<Job> GetAll()
    {
        var jobs = new List<Job>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM jobs";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    jobs.Add(new Job
                    {
                        Id = reader.GetString(0),
                        Title = reader.GetString(1),
                        Min_salary = reader.GetInt32(2),
                        Max_salary = reader.GetInt32(3)
                    });
                }
                reader.Close();
                connection.Close();

                return jobs;
            }
            reader.Close();
            connection.Close();

            return new List<Job>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Job>();
    }

    // GET BY ID: Job
    public Job GetById(int id)
    {

        var job = new Job();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM jobs WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek region dengan menggunakan while loop
                    job.Id = reader.GetString(0);
                    job.Title = reader.GetString(1);
                    job.Min_salary = reader.GetInt32(2);
                    job.Max_salary = reader.GetInt32(3);
                }
                reader.Close();
                connection.Close();

                return new Job();
            }
            reader.Close();
            connection.Close();

            return new Job();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Job();
    }

    // INSERT: Job
    public string Insert(string id, string title, int min_salary, int max_salary)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO jobs VALUES (@id, @title, @min_salary, @max_salary);";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@title", title));
            command.Parameters.Add(new SqlParameter("@min_salary", min_salary));
            command.Parameters.Add(new SqlParameter("@min_salary", min_salary));
            

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

    // UPDATE: Region
    public string Update(string id, string title, int min_salary, int max_salary)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE jobs SET @id = id, @title = title, @min_salary = min_salary, @max_salary = max_salary  WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@title", title));
            command.Parameters.Add(new SqlParameter("@min_salary", min_salary));
            command.Parameters.Add(new SqlParameter("@max_salary", max_salary));
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

    // DELETE: Job
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM jobs WHERE @id = id;";

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
