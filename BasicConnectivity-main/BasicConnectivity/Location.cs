using System.Data.SqlClient;

namespace BasicConnectivity;

public class Location
{
    public int Id { get; set; }
    public string street_address { get; set; }
    public string postal_code { get; set; }
    public string city { get; set; }
    public string state_province { get; set; }
    public string country_id { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Location
    public List<Location> GetAll()
    {
        var locations = new List<Location>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM locations";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    locations.Add(new Location
                    {
                        Id = reader.GetInt32(0),
                        street_address = reader.GetString(1),
                        postal_code = reader.GetString(2),
                        city = reader.GetString(3),
                        state_province = reader.GetString(4),
                        country_id = reader.GetString(5)
                    });
                }
                reader.Close();
                connection.Close();

                return locations;
            }
            reader.Close();
            connection.Close();

            return new List<Location>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Location>();
    }

    // GET BY ID: Location
    public Location GetById(int id)
    {

        var location = new Location();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM locations WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek region dengan menggunakan while loop
                    location.Id = reader.GetInt32(0);
                    location.street_address = reader.GetString(1);
                    location.postal_code = reader.GetString(2);
                    location.city = reader.GetString(3);
                    location.state_province = reader.GetString(4);
                    location.country_id = reader.GetString(5);
                }
                reader.Close();
                connection.Close();

                return new Location();
            }
            reader.Close();
            connection.Close();

            return new Location();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Location();
    }

    // INSERT: Location
    public string Insert(int id, string street_address, string postal_code, string city, string state_province, string country_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO regions VALUES (@id, @street_address, @postal_code, @city, @state_province, @country_id);";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@street_address", street_address));
            command.Parameters.Add(new SqlParameter("@postal_code", postal_code));
            command.Parameters.Add(new SqlParameter("@city", city));
            command.Parameters.Add(new SqlParameter("@state_province", state_province));
            command.Parameters.Add(new SqlParameter("@country_id", country_id));

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

    // UPDATE: Location
    public string Update(int id, string street_address, string postal_code, string city, string state_province, string country_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE locations SET @id = id, @street_address = street_address, @postal_code = postal_code, @city = city, @state_province = state_province, @country_id = country_id WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@street_address", street_address));
            command.Parameters.Add(new SqlParameter("@postal_code", postal_code));
            command.Parameters.Add(new SqlParameter("@city", city));
            command.Parameters.Add(new SqlParameter("@state_province", state_province));
            command.Parameters.Add(new SqlParameter("@country_id", country_id));
            
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

    // DELETE: Location
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM locations WHERE @id = id;";

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
