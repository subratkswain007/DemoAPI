using Entity.Models;

namespace SAL.EmployeeService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetAllEmployeesAsync();
        Task<EmployeeEntity?> GetEmployeeByIdAsync(int id);
        Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity employee);
        Task UpdateEmployeeAsync(EmployeeEntity employee);
        Task DeleteEmployeeAsync(int id);
    }

}
