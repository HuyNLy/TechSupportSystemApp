using Microsoft.AspNetCore.Mvc;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Services.Interfaces;

namespace TechSupportSystemApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketController(ITicketService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _service.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _service.GetTicketByIdAsync(id);

        if (ticket is null)
            return NotFound();

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewTicketDTO dto)
    {
        var created = await _service.CreateTicketAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.ticketId }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTicketAsync(id);
        return NoContent();
    }
}
