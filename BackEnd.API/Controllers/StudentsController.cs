using BackEnd.API.Data;
using BackEnd.API.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.API.Controllers;

public sealed class StudentsController(AppDbContext dbContext) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await dbContext.AddAsync(student);

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditStudent(int id, Student student)
    {
        Student? studentFromDb = await dbContext.Students.FindAsync(id);

        if (studentFromDb is null)
        {
            return BadRequest("Student Not fount");
        }

        studentFromDb.Name = student.Name;
        studentFromDb.Address = student.Address;
        studentFromDb.Email = student.Email;
        studentFromDb.PhoneNumber = student.PhoneNumber;

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await dbContext.Students.SingleOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            return NotFound();
        }

        dbContext.Remove(student);

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return Ok();
        }

        return BadRequest("Unable to delted student.");
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents() => Ok(await dbContext.Students.AsNoTracking().ToListAsync());

    [HttpGet("id")]
    public async Task<IActionResult> GetStudent(Guid id)
    {
        Student? student = await dbContext.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        return Ok(student);
    }
}