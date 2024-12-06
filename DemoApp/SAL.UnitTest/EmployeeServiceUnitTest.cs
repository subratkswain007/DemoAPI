using Moq;
using AutoMapper;
using Xunit;
using FluentAssertions;
using DAL.Models;
using DAL.Repository;
using DAL.UOW;
using Entity.Models;
using Microsoft.Extensions.Logging;
using EmpService = SAL.EmployeeService;

namespace SAL.UnitTest
{
    public class EmployeeServiceUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Employee>> _mockEmployeeRepository;
        private readonly Mock<ILogger<EmpService.EmployeeService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private EmpService.EmployeeService _employeeService;

        public EmployeeServiceUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmployeeRepository = new Mock<IRepository<Employee>>();
            _mockLogger = new Mock<ILogger<EmpService.EmployeeService>>();

            _mockMapper = new Mock<IMapper>();

            // Setup mock to return repository
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnEmployeeEntities()
        {
            // Arrange
            var employeeList = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId=1,Salary=100 },
                new Employee { Id = 2, Name = "Jane Smith", Address = "BBSR", DepartmentId=2,Salary=2  }
            };

            var employeeEntities = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId=1,Salary=100 },
                new EmployeeEntity { Id = 2, Name = "Jane Smith", Address = "BBSR", DepartmentId=2,Salary=2  }
            };

            // Mock GetAllAsync to return employee list
            _mockEmployeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employeeList);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);
            _mockMapper.Setup(mapper => mapper.Map<List<EmployeeEntity>>(employeeList)).Returns(employeeEntities);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);
            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            result.Should().BeEquivalentTo(employeeEntities);  // Ensure it maps correctly
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<EmployeeEntity>>(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployeeEntity_WhenFound()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };
            var employeeEntity = new EmployeeEntity { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };

            // Mock GetByIdAsync to return employee
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);
            _mockMapper.Setup(mapper => mapper.Map<EmployeeEntity>(employee)).Returns(employeeEntity);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(employeeEntity);  // Ensure it maps correctly
            Assert.IsType<EmployeeEntity>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Employee)null);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);
            _mockMapper.Setup(mapper => mapper.Map<EmployeeEntity>((Employee)null)).Returns((EmployeeEntity)null);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync(1);

            // Assert
            result.Should().BeNull();
            Assert.Null(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldReturnEmployeeId_WhenAdded()
        {
            // Arrange
            var employeeEntity = new EmployeeEntity { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };
            var employee = new Employee { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };

            _mockMapper.Setup(mapper => mapper.Map<EmployeeEntity>(It.IsAny<Employee>())).Returns(employeeEntity);
            _mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).ReturnsAsync(employee);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);

            // Act
            var result = await _employeeService.AddEmployeeAsync(employeeEntity);

            // Assert
            result.Should().BeEquivalentTo(employeeEntity);   
            Assert.IsType<EmployeeEntity>(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldReturnNull_Exception()
        {
            // Arrange
            var employeeEntity = new EmployeeEntity
            {
                Id = 0,
                Name = "string",
                Address = "string",
                DepartmentId = 0,
                Salary = 0
            };

            _mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Throws(new Exception("Error Occurred"));
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);
                        
            // Act & Assert
            var result = await Assert.ThrowsAsync<Exception>(() => _employeeService.AddEmployeeAsync(employeeEntity));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _employeeService.AddEmployeeAsync(employeeEntity));
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employeeEntity = new EmployeeEntity { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };
            var employee = new Employee { Id = 1, Name = "John Doe", Address = "Dhenkanal", DepartmentId = 1, Salary = 100 };

            _mockMapper.Setup(mapper => mapper.Map<Employee>(employeeEntity)).Returns(employee);
            _mockEmployeeRepository.Setup(repo => repo.UpdateAsync(employee));
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);

            // Act
            await _employeeService.UpdateEmployeeAsync(employeeEntity);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.UpdateAsync(employee), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldDeleteEmployee()
        {
            // Arrange
            var employeeId = 1;

            _mockEmployeeRepository.Setup(repo => repo.DeleteAsync(employeeId));
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Employee>()).Returns(_mockEmployeeRepository.Object);

            // Initialize the EmployeeService
            _employeeService = new EmpService.EmployeeService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);

            // Act
            await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.DeleteAsync(employeeId), Times.Once);
        }
    }
}
