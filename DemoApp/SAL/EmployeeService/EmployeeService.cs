using AutoMapper;
using DAL.Models;
using DAL.Repository;
using DAL.UOW;
using Entity.Models;
using Microsoft.Extensions.Logging;

namespace SAL.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;  

        public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // Private helper method to get the Employee repository
        private IRepository<Employee> GetEmployeeRepository()
        {
            return _unitOfWork.GetRepository<Employee>();
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeesAsync()
        {
            _logger.LogInformation("Fetching all employees");
            // Access the Employee repository through UnitOfWork
            var employees = await GetEmployeeRepository().GetAllAsync();

            // Use AutoMapper to map Employee to EmployeeEntity
            var employeeEntities = _mapper.Map<List<EmployeeEntity>>(employees);
            return employeeEntities;
        }

        public async Task<EmployeeEntity?> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching employee by ID: {id}");
            var employee =await GetEmployeeRepository().GetByIdAsync(id);

            if (employee == null) return null;

            // Use AutoMapper to map Employee to EmployeeResponse
            var employeeResponse = _mapper.Map<EmployeeEntity>(employee);

            return employeeResponse;
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity employee)
        {
            _logger.LogInformation("Adding a new employee");
            var employeeMapObject = _mapper.Map<Employee>(employee);
           var insertedRec = await GetEmployeeRepository().AddAsync(employeeMapObject);

            if (insertedRec == null) return null;
            // Use AutoMapper to map Employee to EmployeeResponse
            var employeeResponse = _mapper.Map<EmployeeEntity>(insertedRec);
            return employeeResponse;
        }

        public async Task UpdateEmployeeAsync(EmployeeEntity employee)
        {
            _logger.LogInformation($"Updating employee ID: {employee.Id}");
            var employeeMapObject = _mapper.Map<Employee>(employee);
            await GetEmployeeRepository().UpdateAsync(employeeMapObject);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation($"Deleting employee ID: {id}");
            await GetEmployeeRepository().DeleteAsync(id);
        }
    }

}
