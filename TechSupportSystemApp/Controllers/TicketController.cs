using Microsoft.AspNetCore.Mvc;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
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
        => Ok(await _service.GetAllTicketsAsync());

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(TicketStatus status)
        => Ok(await _service.GetTicketsByStatusAsync(status));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _service.GetTicketByIdAsync(id);
        if (ticket is null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewTicketDTO dto)
    {
        var created = await _service.CreateTicketAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.TicketId }, created);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTicketDTO dto)
    {
        await _service.UpdateTicketAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTicketAsync(id);
        return NoContent();
    }
}