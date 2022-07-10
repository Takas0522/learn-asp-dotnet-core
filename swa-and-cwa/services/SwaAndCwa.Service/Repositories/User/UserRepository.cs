using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SwaAndCwa.Service.Models;
using Dapper;
using System.Data;

namespace SwaAndCwa.Service.Repsitories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(
        IConfiguration config
    )
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<User>> GetDatasAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var res = await connection.QueryAsync<User>("[dbo].[GetUsers]", commandType: CommandType.StoredProcedure);
            return res;
        }
    }

    public async Task<User> GetDataAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var param = new { id };
            var res = await connection.QueryFirstAsync<User>("[dbo].[GetUser]", param, commandType: CommandType.StoredProcedure);
            return res;
        }
    }

    public async Task RegisterAsync(User data)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var param = new
            {
                id = data.Id,
                name = data.Name
            };
            await connection.ExecuteAsync("[dbo].[RegisterUser]", param, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var param = new { id };
            await connection.ExecuteAsync("[dbo].[DeleteUser]", param, commandType: CommandType.StoredProcedure);
        }
    }

}