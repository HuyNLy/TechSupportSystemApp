using Microsoft.AspNetCore.Mvc;
using TechSupportSystemApp.Data;
using TechSupportSystemApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TechSupportSystemApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return employee;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.eId }, employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
