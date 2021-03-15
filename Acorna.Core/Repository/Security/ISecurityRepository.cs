using System.Threading.Tasks;
public interface ISecurityRepository
{
    Task<object> GetUsersListAsync();
    Task<object> GetUserBySearchNameAsync(string userName);
    Task<object> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
}