using System.Net.NetworkInformation;
using Dapper;
using Npgsql;
using StudentManagementApi.Models;
public class StudentRepository
{
  private readonly IConfiguration _config;
  public StudentRepository(IConfiguration config)
  {
    _config = config;
  }
  private NpgsqlConnection CreateConnection()
      => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

  public async Task<IEnumerable<Students>> GetAllAsync()
  {
    using var conn = CreateConnection();
    var sql = "SELECT * FROM Students";
    return await conn.QueryAsync<Students>(sql);
  }
  public async Task<Students> GetByIdAsync(int id)
  {
    using var conn = CreateConnection();
    var sql = "SELECT * FROM Students WHERE Id = @Id";
    return await conn.QueryFirstOrDefaultAsync<Students>(sql, new { Id = id });
  }
  public async Task<int> CreateAsync(Students student)
  {
    using var conn = CreateConnection();
    var sql = @"INSERT INTO Students (Firstname, Lastname, DateOfBirth, Age, CreatedDate) 
                VALUES (@Firstname, @Lastname, @DateOfBirth, @Age, @CreatedDate) RETURNING Id";
    return await conn.ExecuteScalarAsync<int>(sql, student);
  }
  public async Task<bool> UpdateAsync(Students student)
  {
    using var conn = CreateConnection();
    var sql = @"UPDATE Students SET Firstname = @Firstname, Lastname = @Lastname, 
                DateOfBirth = @DateOfBirth, Age = @Age WHERE Id = @Id";
    var rows = await conn.ExecuteAsync(sql, new
    {
      student.Firstname,
      student.Lastname,
      student.DateOfBirth,
      student.Age,
      Id = student.Id
    });
    return rows > 0;
  }
  public async Task<bool> DeleteAsync(int id)
  {
    using var conn = CreateConnection();
    var sql = "DELETE FROM Students WHERE Id = @Id";
    var rows = await conn.ExecuteAsync(sql, new { Id = id });
    return rows > 0;
  }
  
}