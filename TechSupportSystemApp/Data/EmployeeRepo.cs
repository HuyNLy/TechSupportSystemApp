using Microsoft.EntityFrameworkCore;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public class EmployeeRepo : IEmployeeRepo
{
    private readonly AppDbContext _context;

    public EmployeeRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
        => await _context.Employees
            .Include(e => e.Tickets)
            .ToListAsync();

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
        => await _context.Employees
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.EId == id);

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task DeleteEmployeeAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}