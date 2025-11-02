using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Models;
[ApiController]
[Route("api/[controller]")]
public class StudentsControllers : ControllerBase
{
  private readonly StudentRepository _repo;
  public StudentsControllers(IConfiguration config)
  {
    _repo = new StudentRepository(config);
  }
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var students = await _repo.GetAllAsync();
    return Ok(students);
  }
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var student = await _repo.GetByIdAsync(id);
    if (student == null)
      return NotFound();
    return Ok(student);
  }
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] Students student)
  {
    student.CreatedDate = DateTime.UtcNow;
    var newId = await _repo.CreateAsync(student);
    return CreatedAtAction(nameof(Get), new { id = newId }, student);
  }
  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] Students student)
  {
    var existingStudent = await _repo.GetByIdAsync(id);
    if (existingStudent == null)
      return NotFound();
    student.Id = id;
    var updated = await _repo.UpdateAsync(student);
    if (!updated)
      return StatusCode(500, "A problem happened while handling your request.");
    return NoContent();
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var existingStudent = await _repo.GetByIdAsync(id);
    if (existingStudent == null)
      return NotFound();
    var deleted = await _repo.DeleteAsync(id);
    if (!deleted)
      return StatusCode(500, "A problem happened while handling your request.");
    return NoContent();
  }
}