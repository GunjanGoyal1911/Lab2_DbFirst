namespace Lab2_DbFirst.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeFirstName { get; set; }

    public string? EmployeeLastName { get; set; }

    public string? EmployeeEmail { get; set; }

    public int? EmployeeStoreId { get; set; }

    public virtual Store? EmployeeStore { get; set; }
}
