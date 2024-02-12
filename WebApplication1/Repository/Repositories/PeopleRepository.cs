
using WebApplication1.Data;
using WebApplication1.Models;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository.IRepository;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using WebApplication1.Controllers;
using System.Threading;
using System.Data.Entity.Migrations;

namespace WebApplication1.Repository.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly WebApplication1Context _context;

        public PeopleRepository(WebApplication1Context context)
        {
            _context = context;
        }

        public async Task<PagedResult<People>> GetPagedPeopleAsync(int offset, int page, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
 

            var query = _context.People.OrderBy(x => x.FirstName)
                .Select(p => p)
                .Skip(offset)
                .Take(pageSize);

            var result = new PagedResult<People>
            {
                TotalItems = await _context.People.CountAsync(),
                PageNumber = page,
                PageSize = pageSize,
                Data = await query.ToListAsync()
            };

            return result;
        }

        public async Task<List<People>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }
        public async Task<List<People>> SearchPeopleAsync(string searchString)
        {
            return await _context.People
                .Where(m => m.FirstName.Contains(searchString))
                .ToListAsync();
        }


        public async Task<People> GetPersonByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task AddPersonAsync(People person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(People person)
        {
            _context.People.AddOrUpdate(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

    }
}
