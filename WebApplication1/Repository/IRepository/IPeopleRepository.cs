using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Person.Models;

namespace Person.Repositories
{
    public interface IPeopleRepository 
    {
        Task<PagedResult<People>> GetPagedPeopleAsync(int offset, int page, int pageSize, CancellationToken cancellationToken = default(CancellationToken));
        Task<People> GetPersonByIdAsync(int id);
        Task AddPersonAsync(People person);
        Task UpdatePersonAsync(People person);
        Task DeletePersonAsync(int id);
        bool PersonExists(int id);
        Task<List<People>> GetPeopleAsync();
        Task<List<People>> SearchPeopleAsync(string searchString);
    }
}
