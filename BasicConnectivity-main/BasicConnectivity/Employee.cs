using System.Data.SqlClient;

namespace BasicConnectivity;

public class Employee
{
    public int Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Email { get; set; }
    public string Phone_number { get; set; }
    public DateTime Hire_date { get; set; }
    public int Salary { get; set; }
    public decimal Commision_pct { get; set; }
    public int Manager_id { get; set; }
    public string Job_id { get; set; }
    public int Department_id { get; set; }

    private readonly string connectionString =
        "Data Source=IQSALGAFHARI;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Employee
    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM employees";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = reader.GetInt32(0),
                        First_name = reader.GetString(1),
                        Last_name = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone_number = reader.GetString(4),
                        Hire_date = reader.GetDateTime(5),
                        Salary = reader.GetInt32(6),
                        Commision_pct = reader.GetDecimal(7),
                        Manager_id = reader.GetInt32(8),
                        Job_id = reader.GetString(9),
                        Department_id = reader.GetInt32(10)
                    });
                }
                reader.Close();
                connection.Close();

                return employees;
            }
            reader.Close();
            connection.Close();

            return new List<Employee>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Employee>();
    }

    // GET BY ID: Employee
    public Employee GetById(int id)
    {

        var employee = new Employee();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM employees WHERE @id = id";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //menambahkan isi untuk objek dengan menggunakan while loop
                    employee.Id = reader.GetInt32(0);
                    employee.First_name = reader.GetString(1);
                    employee.Last_name = reader.GetString(2);
                    employee.Email = reader.GetString(3);
                    employee.Phone_number = reader.GetString(4);
                    employee.Hire_date = reader.GetDateTime(5);
                    employee.Salary = reader.GetInt32(6);
                    employee.Commision_pct = reader.GetDecimal(7);
                    employee.Manager_id = reader.GetInt32(8);
                    employee.Job_id = reader.GetString(9);
                    employee.Department_id = reader.GetInt32(10);
                }
                reader.Close();
                connection.Close();

                return new Employee();
            }
            reader.Close();
            connection.Close();

            return new Employee();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Employee();
    }

    // INSERT: Region
    public string Insert(string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, decimal commision_pct, int manager_id, string job_id, int department_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO regions VALUES (@first_name, @last_name, @email, @phone_number, @hire_date, @salary, @commision_pct, @manager_id, @job_id, @department_id);";

        try
        {
            command.Parameters.Add(new SqlParameter("@first_name", first_name));
            command.Parameters.Add(new SqlParameter("@last_name", last_name));
            command.Parameters.Add(new SqlParameter("@email", email));
            command.Parameters.Add(new SqlParameter("@phone_number", phone_number));
            command.Parameters.Add(new SqlParameter("@hire_date", hire_date));
            command.Parameters.Add(new SqlParameter("@salary", salary));
            command.Parameters.Add(new SqlParameter("@commision_pct", commision_pct));
            command.Parameters.Add(new SqlParameter("@manager_id", manager_id));
            command.Parameters.Add(new SqlParameter("@job_id", job_id));
            command.Parameters.Add(new SqlParameter("@department_id", department_id));

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

    // UPDATE: Employee
    public string Update(int id, string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, decimal commision_pct, int manager_id, string job_id, int department_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE employees SET @first_name = first_name, @last_name = last_name, @email = email, @phone_number = phone_number, @hire_date = hire_date, @salary = salary, @commision_pct = commision_pct, @manager_id = manager_id, @job_id = job_id, @department_id = department_id WHERE @id = id;";

        try
        {
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@first_name", first_name));
            command.Parameters.Add(new SqlParameter("@last_name", last_name));
            command.Parameters.Add(new SqlParameter("@email", email));
            command.Parameters.Add(new SqlParameter("@phone_number", phone_number));
            command.Parameters.Add(new SqlParameter("@hire_date", hire_date));
            command.Parameters.Add(new SqlParameter("@salary", salary));
            command.Parameters.Add(new SqlParameter("@commision_pct", commision_pct));
            command.Parameters.Add(new SqlParameter("@manager_id", manager_id));
            command.Parameters.Add(new SqlParameter("@job_id", job_id));
            command.Parameters.Add(new SqlParameter("@department_id", department_id));
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

    // DELETE: Employee
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM employees WHERE @id = id;";

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
