using Application.Common.Models;
using Application.Domain;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IHumanResourceRepository
    {
        Task<HumanResource> GetByIdAsync(int humanResourceId);
        Task<PaginatedList<HumanResource>> GetAllAsync(int? statusId = null, int? departmentId = null, int pageNumber = 1);
        Task<int> AddAsync(HumanResource humanResource);
        Task<int> UpdateAsync(HumanResource humanResource);
        Task<int> DeleteAsync(int humanResourceId);
        int? CheckEmailExists(string email);
        int? CheckEmployeeNumberExists(int employeeNumber);
    }
}
