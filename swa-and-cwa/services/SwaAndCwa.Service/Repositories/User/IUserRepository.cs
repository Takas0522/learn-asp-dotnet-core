using SwaAndCwa.Service.Models;

namespace SwaAndCwa.Service.Repsitories;

public interface IUserRepository
{
    Task DeleteAsync(int id);
    Task<User> GetDataAsync(int id);
    Task<IEnumerable<User>> GetDatasAsync();
    Task RegisterAsync(User data);
}