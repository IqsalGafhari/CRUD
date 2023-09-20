using System.Data.SqlClient;

namespace BasicConnectivity;

public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Region_id { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Country
    public List<Country> GetAll()
    {
        var countries = new List<Country>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM countries";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countries.Add(new Country
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Region_id = reader.GetInt32(2)
                    });
                }
                reader.Close();
                connection.Close();

                return countries;
            }
            reader.Close();
            connection.Close();

            return new List<Country>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Country>();
    }

    // GET BY ID: Country
    public Country GetById(string id)
    {

        var country = new Country();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM countries WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek region dengan menggunakan while loop
                    country.Id = reader.GetString(0);
                    country.Name = reader.GetString(1);
                    country.Region_id = reader.GetInt32(2);
                }
                reader.Close();
                connection.Close();

                return new Country();
            }
            reader.Close();
            connection.Close();

            return new Country();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Country();
    }

    // INSERT: Country
    public string Insert(string name, int region_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO countries VALUES (@name, @region_id);";
        
        try
        {
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@region_id", region_id));
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

    // UPDATE: Country
    public string Update(string id, string name, int region_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE regions SET @name = name, @region_id = region_id WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@region_id", region_id));
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

    // DELETE: Country
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM countries WHERE @id = id;";

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
