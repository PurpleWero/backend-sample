using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Employee
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string JobPosition { get; set; }
    public decimal Salary { get; set; }
}
