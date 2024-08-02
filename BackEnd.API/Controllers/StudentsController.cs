using BackEnd.API.Data;
using BackEnd.API.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents() => Ok(await dbContext.Students.AsNoTracking().ToListAsync());

}

