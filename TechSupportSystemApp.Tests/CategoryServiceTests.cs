using Moq;
using TechSupportSystemApp.Data;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Implementations;

namespace TechSupportSystemApp.Tests;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepo> _repoMock;
    private readonly CategoryService _sut;

    public CategoryServiceTests()
    {
        _repoMock = new Mock<ICategoryRepo>();
        _sut = new CategoryService(_repoMock.Object);
    }

    // Test 1: Category contains correct tickets in response
    [Fact]
    public async Task GetByIdAsync_Found_ReturnsCategoryWithTickets()
    {
        _repoMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(new Category
        {
            CatId = 1,
            CatName = "Hardware",
            Tickets = new List<Ticket>
            {
                new Ticket { TicketTitle = "Monitor broken" },
                new Ticket { TicketTitle = "Keyboard issue" }
            }
        });

        var result = await _sut.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Hardware", result!.CatName);
        Assert.Equal(2, result.Tickets.Count);
        Assert.Contains("Monitor broken", result.Tickets);
        Assert.Contains("Keyboard issue", result.Tickets);
        _repoMock.Verify(r => r.GetCategoryByIdAsync(1), Times.Once);
    }

    // Test 2: Category with no tickets returns empty list
    [Fact]
    public async Task GetByIdAsync_Found_ReturnsEmptyTicketList_WhenNoTickets()
    {
        _repoMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(new Category
        {
            CatId = 1,
            CatName = "Hardware",
            Tickets = new List<Ticket>()
        });

        var result = await _sut.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Empty(result!.Tickets);
        _repoMock.Verify(r => r.GetCategoryByIdAsync(1), Times.Once);
    }

    // Test 3: GetAllAsync returns all categories each with their tickets
    [Fact]
    public async Task GetAllAsync_ReturnsCategoriesWithTickets()
    {
        _repoMock.Setup(r => r.GetAllCategoriesAsync()).ReturnsAsync(new List<Category>
        {
            new Category
            {
                CatId = 1,
                CatName = "Hardware",
                Tickets = new List<Ticket>
                {
                    new Ticket { TicketTitle = "Monitor broken" }
                }
            },
            new Category
            {
                CatId = 2,
                CatName = "Software",
                Tickets = new List<Ticket>
                {
                    new Ticket { TicketTitle = "App crash" },
                    new Ticket { TicketTitle = "Login issue" }
                }
            }
        });

        var result = await _sut.GetAllAsync();
        var list = result.ToList();

        Assert.Equal(2, list.Count);

        // Check Hardware category
        var hardware = list.First(c => c.CatName == "Hardware");
        Assert.Single(hardware.Tickets);
        Assert.Contains("Monitor broken", hardware.Tickets);

        // Check Software category
        var software = list.First(c => c.CatName == "Software");
        Assert.Equal(2, software.Tickets.Count);
        Assert.Contains("App crash", software.Tickets);
        Assert.Contains("Login issue", software.Tickets);

        _repoMock.Verify(r => r.GetAllCategoriesAsync(), Times.Once);
    }

    // Theory Test 4: Multiple category IDs that don't exist return null
    [Theory]
    [InlineData(99)]
    [InlineData(100)]
    [InlineData(999)]
    public async Task GetByIdAsync_NotFound_ReturnsNull(int id)
    {
        _repoMock.Setup(r => r.GetCategoryByIdAsync(id)).ReturnsAsync((Category?)null);

        var result = await _sut.GetByIdAsync(id);

        Assert.Null(result);
        _repoMock.Verify(r => r.GetCategoryByIdAsync(id), Times.Once);
    }

    // Theory Test 5: Delete returns false for multiple missing IDs
    [Theory]
    [InlineData(99)]
    [InlineData(100)]
    [InlineData(999)]
    public async Task DeleteAsync_NotFound_ReturnsFalse(int id)
    {
        _repoMock.Setup(r => r.GetCategoryByIdAsync(id)).ReturnsAsync((Category?)null);

        var result = await _sut.DeleteAsync(id);

        Assert.False(result);
        _repoMock.Verify(r => r.GetCategoryByIdAsync(id), Times.Once);
        _repoMock.Verify(r => r.DeleteCategoryAsync(It.IsAny<Category>()), Times.Never);
    }

    // Fact Test 6: Delete returns true and calls repo when category found
    [Fact]
    public async Task DeleteAsync_Found_ReturnsTrueAndCallsRepo()
    {
        var category = new Category
        {
            CatId = 1,
            CatName = "Hardware",
            Tickets = new List<Ticket>()
        };

        _repoMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(category);
        _repoMock.Setup(r => r.DeleteCategoryAsync(category)).Returns(Task.CompletedTask);

        var result = await _sut.DeleteAsync(1);

        Assert.True(result);
        _repoMock.Verify(r => r.GetCategoryByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.DeleteCategoryAsync(category), Times.Once);
    }
}