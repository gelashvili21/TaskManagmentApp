using Microsoft.AspNetCore.Mvc;
using TaskManagmentApp.Models;
using TaskManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, Project updatedProject)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        project.Name = updatedProject.Name;
        project.Description = updatedProject.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> filter(int id, Project getporject)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        project.Name = getporject.Name;
        project.Description = getporject.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> SaveLog(Project project ,Logs log)
    {
        _context.Logs.Add(project , log);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
    }
}
