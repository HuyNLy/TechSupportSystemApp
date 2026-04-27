using Moq;
using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Implementations;

namespace TechSupportSystemApp.Tests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepo> _repoMock;
    private readonly EmployeeService _sut;

    public EmployeeServiceTests()
    {
        _repoMock = new Mock<IEmployeeRepo>();
        _sut = new EmployeeService(_repoMock.Object);
    }

    // Theory Test 1: Multiple invalid names that should still create employee
    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Charlie")]
    public async Task CreateAsync_ValidName_ReturnsEmployeeWithCorrectName(string name)
    {
        var dto = new NewEmployeeDTO { EName = name };
        var employee = new Employee { EId = 1, EName = name, Tickets = new List<Ticket>() };

        _repoMock.Setup(r => r.CreateEmployeeAsync(It.IsAny<Employee>()))
            .ReturnsAsync(employee);

        var result = await _sut.CreateAsync(dto);

        Assert.Equal(name, result.EName);
        _repoMock.Verify(r => r.CreateEmployeeAsync(It.IsAny<Employee>()), Times.Once);
    }

    // Theory Test 2: Multiple valid IDs that all return null (not found)
    [Theory]
    [InlineData(99)]
    [InlineData(100)]
    [InlineData(999)]
    public async Task GetByIdAsync_NotFound_ReturnsNull(int id)
    {
        _repoMock.Setup(r => r.GetEmployeeByIdAsync(id)).ReturnsAsync((Employee?)null);

        var result = await _sut.GetByIdAsync(id);

        Assert.Null(result);
        _repoMock.Verify(r => r.GetEmployeeByIdAsync(id), Times.Once);
    }

    // Theory Test 3: Delete returns false for multiple missing IDs
    [Theory]
    [InlineData(99)]
    [InlineData(100)]
    [InlineData(999)]
    public async Task DeleteAsync_NotFound_ReturnsFalse(int id)
    {
        _repoMock.Setup(r => r.GetEmployeeByIdAsync(id)).ReturnsAsync((Employee?)null);

        var result = await _sut.DeleteAsync(id);

        Assert.False(result);
        _repoMock.Verify(r => r.GetEmployeeByIdAsync(id), Times.Once);
        _repoMock.Verify(r => r.DeleteEmployeeAsync(It.IsAny<Employee>()), Times.Never);
    }

    // Fact Test 4: GetAllAsync returns all employees mapped correctly
    [Fact]
    public async Task GetAllAsync_ReturnsMappedDTOs()
    {
        _repoMock.Setup(r => r.GetAllEmployeesAsync()).ReturnsAsync(new List<Employee>
        {
            new Employee { EId = 1, EName = "Alice", Tickets = new List<Ticket>
            {
                new Ticket { TicketTitle = "Monitor broken" }
            }},
            new Employee { EId = 2, EName = "Bob", Tickets = new List<Ticket>() }
        });

        var result = await _sut.GetAllAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, e => e.EName == "Alice");
        Assert.Contains(result, e => e.EName == "Bob");
        _repoMock.Verify(r => r.GetAllEmployeesAsync(), Times.Once);
    }

    // Fact Test 5: GetByIdAsync returns mapped DTO with tickets when found
    [Fact]
    public async Task GetByIdAsync_Found_ReturnsMappedDTOWithTickets()
    {
        _repoMock.Setup(r => r.GetEmployeeByIdAsync(1)).ReturnsAsync(new Employee
        {
            EId = 1,
            EName = "Alice",
            Tickets = new List<Ticket>
            {
                new Ticket { TicketTitle = "Monitor broken" },
                new Ticket { TicketTitle = "Keyboard issue" }
            }
        });

        var result = await _sut.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Alice", result!.EName);
        Assert.Equal(2, result.Tickets.Count);
        Assert.Contains("Monitor broken", result.Tickets);
        _repoMock.Verify(r => r.GetEmployeeByIdAsync(1), Times.Once);
    }

    // Fact Test 6: DeleteAsync returns true and calls repo when employee found
    [Fact]
    public async Task DeleteAsync_Found_ReturnsTrueAndCallsRepo()
    {
        var employee = new Employee 
        { 
            EId = 1, 
            EName = "Alice", 
            Tickets = new List<Ticket>() 
        };

        _repoMock.Setup(r => r.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);
        _repoMock.Setup(r => r.DeleteEmployeeAsync(employee)).Returns(Task.CompletedTask);

        var result = await _sut.DeleteAsync(1);

        Assert.True(result);
        _repoMock.Verify(r => r.GetEmployeeByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.DeleteEmployeeAsync(employee), Times.Once);
    }
}