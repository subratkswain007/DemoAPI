﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? DepartmentName { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
