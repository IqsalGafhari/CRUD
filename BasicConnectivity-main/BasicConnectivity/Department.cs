using System.Data.SqlClient;

namespace BasicConnectivity;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Location_id { get; set; }
    public int Manager_id { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Department
    public List<Department> GetAll()
    {
        var departments = new List<Department>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM departments";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    departments.Add(new Department
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location_id = reader.GetInt32(2),
                        Manager_id = reader.GetInt32(3)
                    });
                }
                reader.Close();
                connection.Close();

                return departments;
            }
            reader.Close();
            connection.Close();

            return new List<Department>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Department>();
    }

    // GET BY ID: Department
    public Department GetById(int id)
    {

        var department = new Department();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM departments WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek dengan menggunakan while loop
                    department.Id = reader.GetInt32(0);
                    department.Name = reader.GetString(1);
                    department.Location_id = reader.GetInt32(2);
                    department.Manager_id = reader.GetInt32(3);
                }
                reader.Close();
                connection.Close();

                return new Department();
            }
            reader.Close();
            connection.Close();

            return new Department();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Department();
    }

    // INSERT: Department
    public string Insert(string name, int location_id, int manager_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO departments VALUES (@name, @location_id, @manager_id);";

        try
        {
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@location_id", location_id));
            command.Parameters.Add(new SqlParameter("@manager_id", manager_id));

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

    // UPDATE: Department
    public string Update(int id, string name, int location_id, int manager_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE regions SET @name = name, @location_id = location_id, @manager_id = manager_id WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@location_id", location_id));
            command.Parameters.Add(new SqlParameter("@manager_id", manager_id));
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

    // DELETE: Departments
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM departments WHERE @id = id;";

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
