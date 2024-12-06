using System.ComponentModel.DataAnnotations;

namespace Entity.Models
{
    public class EmployeeEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string? Address { get; set; }

        public int? Salary { get; set; }

    }
}
