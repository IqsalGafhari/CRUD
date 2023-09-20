//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var region = new Region();
        
        var getAllRegion = region.GetAll();

        if (getAllRegion.Count > 0)
        {
            foreach (var region1 in getAllRegion)
            {
                Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }

        /*var insertResult = region.Insert("Region 5");
        int.TryParse(insertResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("Insert Success");
        }
        else 
        {
            Console.WriteLine("Insert Failed");
            Console.WriteLine(insertResult);
        }
    }*/

    /*private static void Main()
    {
        var country = new Country();

        var getAllCountry = country.GetAll();

        if (getAllCountry.Count > 0)
        {
            foreach (var country1 in getAllCountry)
            {
                Console.WriteLine($"ID: {country1.Id}, Name: {country1.Name}, Region ID: {country1.Region_id}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }
    }

    /*private static void Main()
    {
        var department = new Department();

        var getAllDepartment = department.GetAll();

        if (getAllDepartment.Count > 0)
        {
            foreach (var department1 in getAllDepartment)
            {
                Console.WriteLine($"Id: {department1.Id}, Name: {department1.Name}, Location_id: {department1.Location_id}, Manager_id: {department1.Manager_id}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }*/
    }
}
