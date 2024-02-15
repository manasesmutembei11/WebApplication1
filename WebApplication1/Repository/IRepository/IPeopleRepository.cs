using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using MvcPaging;
using PagedList.Mvc;

namespace WebApplication1.Repository.IRepository
{
    public interface IPeopleRepository 
    {
       // Task<PagedList<People>> GetPagedPeopleAsync(int offset, int page, int pageSize, CancellationToken cancellationToken = default(CancellationToken));
        Task<People> GetPersonByIdAsync(int id);
        Task AddPersonAsync(People person);
        Task UpdatePersonAsync(People person);
        Task DeletePersonAsync(int id);
        bool PersonExists(int id);
        Task<List<People>> GetPeopleAsync();
        Task<List<People>> SearchPeopleAsync(string searchString);
    }
}
