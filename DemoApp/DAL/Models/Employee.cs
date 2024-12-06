using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int DepartmentId { get; set; }

    public string? Address { get; set; }

    public int? Salary { get; set; }

    public virtual Department Department { get; set; } = null!;
}
