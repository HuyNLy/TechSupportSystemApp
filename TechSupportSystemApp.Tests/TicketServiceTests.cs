using Moq;
using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Implementations;

namespace TechSupportSystemApp.Tests;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepo> _repoMock;
    private readonly TicketService _sut;

    public TicketServiceTests()
    {
        _repoMock = new Mock<ITicketRepo>();
        _sut = new TicketService(_repoMock.Object);
    }

    // Test 1: DeleteTicketAsync with invalid id never touches repo
    [Fact]
    public async Task DeleteTicketAsync_InvalidId_ThrowsAndNeverTouchesRepo()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.DeleteTicketAsync(0)
        );

        _repoMock.Verify(r => r.GetTicketByIdAsync(It.IsAny<int>()), Times.Never);
        _repoMock.Verify(r => r.DeleteTicketAsync(It.IsAny<Ticket>()), Times.Never);
    }

    // Test 2: DeleteTicketAsync ticket not found throws KeyNotFoundException
    [Fact]
    public async Task DeleteTicketAsync_TicketNotFound_ThrowsKeyNotFoundException()
    {
        _repoMock.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.DeleteTicketAsync(99)
        );

        _repoMock.Verify(r => r.GetTicketByIdAsync(99), Times.Once);
        _repoMock.Verify(r => r.DeleteTicketAsync(It.IsAny<Ticket>()), Times.Never);
    }

    // Test 3: DeleteTicketAsync valid id deletes ticket
    [Fact]
    public async Task DeleteTicketAsync_ValidId_DeletesTicket()
    {
        var ticket = new Ticket
        {
            TicketId = 1,
            TicketTitle = "Test",
            Employee = new Employee { EName = "Alice" },
            Categories = new List<Category>()
        };

        _repoMock.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);
        _repoMock.Setup(r => r.DeleteTicketAsync(ticket)).Returns(Task.CompletedTask);

        await _sut.DeleteTicketAsync(1);

        _repoMock.Verify(r => r.GetTicketByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.DeleteTicketAsync(ticket), Times.Once);
    }

    // Test 4: GetTicketByIdAsync returns null when not found
    [Fact]
    public async Task GetTicketByIdAsync_NotFound_ReturnsNull()
    {
        _repoMock.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        var result = await _sut.GetTicketByIdAsync(99);

        Assert.Null(result);
        _repoMock.Verify(r => r.GetTicketByIdAsync(99), Times.Once);
    }

    // Test 5: GetTicketByIdAsync returns mapped DTO when found
    [Fact]
    public async Task GetTicketByIdAsync_Found_ReturnsMappedDTO()
    {
        _repoMock.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(new Ticket
        {
            TicketId = 1,
            TicketTitle = "Fix Bug",
            TicketDescription = "Something broke",
            Status = TicketStatus.Open,
            Priority = TicketPriority.High,
            Employee = new Employee { EName = "Bob" },
            Categories = new List<Category> { new Category { CatName = "Software" } }
        });

        var result = await _sut.GetTicketByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Fix Bug", result!.TicketTitle);
        Assert.Equal("Bob", result.EmployeeName);
        Assert.Equal(TicketPriority.High, result.Priority);
        Assert.Contains("Software", result.Categories);
        _repoMock.Verify(r => r.GetTicketByIdAsync(1), Times.Once);
    }

    // Test 6: GetAllTicketsAsync returns mapped DTOs
    [Fact]
    public async Task GetAllTicketsAsync_ReturnsMappedDTOs()
    {
        _repoMock.Setup(r => r.GetAllTicketsAsync()).ReturnsAsync(new List<Ticket>
        {
            new Ticket
            {
                TicketId = 1,
                TicketTitle = "Test Ticket",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Low,
                Employee = new Employee { EName = "Alice" },
                Categories = new List<Category>()
            }
        });

        var result = await _sut.GetAllTicketsAsync();

        Assert.Single(result);
        Assert.Equal("Test Ticket", result[0].TicketTitle);
        Assert.Equal("Alice", result[0].EmployeeName);
        _repoMock.Verify(r => r.GetAllTicketsAsync(), Times.Once);
    }

    // Test 7: GetTicketsByStatusAsync returns only matching status
    [Fact]
    public async Task GetTicketsByStatusAsync_ReturnsOnlyMatchingStatus()
    {
        _repoMock.Setup(r => r.GetTicketsByStatusAsync(TicketStatus.Closed))
            .ReturnsAsync(new List<Ticket>
            {
                new Ticket
                {
                    TicketId = 2,
                    TicketTitle = "Old Issue",
                    Status = TicketStatus.Closed,
                    Priority = TicketPriority.Low,
                    Employee = new Employee { EName = "Alice" },
                    Categories = new List<Category>()
                }
            });

        var result = await _sut.GetTicketsByStatusAsync(TicketStatus.Closed);

        Assert.Single(result);
        Assert.Equal(TicketStatus.Closed, result[0].Status);
        _repoMock.Verify(r => r.GetTicketsByStatusAsync(TicketStatus.Closed), Times.Once);
    }

    // Test 8: GetTicketsByPriorityAsync returns only matching priority
    [Fact]
    public async Task GetTicketsByPriorityAsync_ReturnsOnlyMatchingPriority()
    {
        _repoMock.Setup(r => r.GetTicketsByPriorityAsync(TicketPriority.High))
            .ReturnsAsync(new List<Ticket>
            {
                new Ticket
                {
                    TicketId = 3,
                    TicketTitle = "Critical Issue",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.High,
                    Employee = new Employee { EName = "Bob" },
                    Categories = new List<Category>()
                }
            });

        var result = await _sut.GetTicketsByPriorityAsync(TicketPriority.High);

        Assert.Single(result);
        Assert.Equal(TicketPriority.High, result[0].Priority);
        _repoMock.Verify(r => r.GetTicketsByPriorityAsync(TicketPriority.High), Times.Once);
    }

    // Test 9: UpdateTicketAsync throws when ticket not found
    [Fact]
    public async Task UpdateTicketAsync_TicketNotFound_ThrowsKeyNotFoundException()
    {
        _repoMock.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.UpdateTicketAsync(99, new UpdateTicketDTO { TicketTitle = "New" })
        );

        _repoMock.Verify(r => r.GetTicketByIdAsync(99), Times.Once);
        _repoMock.Verify(r => r.UpdateTicketAsync(), Times.Never);
    }

    // Test 10: UpdateTicketAsync only updates provided fields
    [Fact]
    public async Task UpdateTicketAsync_OnlyUpdatesProvidedFields()
    {
        var ticket = new Ticket
        {
            TicketId = 1,
            TicketTitle = "Original Title",
            TicketDescription = "Original Desc",
            Status = TicketStatus.Open,
            Priority = TicketPriority.Low,
            Employee = new Employee { EName = "Alice" },
            Categories = new List<Category>()
        };

        _repoMock.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);
        _repoMock.Setup(r => r.UpdateTicketAsync()).Returns(Task.CompletedTask);

        await _sut.UpdateTicketAsync(1, new UpdateTicketDTO { Status = TicketStatus.Closed });

        Assert.Equal("Original Title", ticket.TicketTitle);
        Assert.Equal("Original Desc", ticket.TicketDescription);
        Assert.Equal(TicketStatus.Closed, ticket.Status);
        Assert.Equal(TicketPriority.Low, ticket.Priority);
        _repoMock.Verify(r => r.GetTicketByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.UpdateTicketAsync(), Times.Once);
    }
}