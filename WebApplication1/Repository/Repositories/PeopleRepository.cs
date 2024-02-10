using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;

namespace Person.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly PersonContext _context;

        public PeopleRepository(PersonContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<People>> GetPagedPeopleAsync(int offset, int page, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = _context.Person.OrderBy(x => x.FirstName)
                .Select(p => p)
                .Skip(offset)
                .Take(pageSize);

            var result = new PagedResult<People>
            {
                TotalItems = await _context.Person.CountAsync(),
                PageNumber = page,
                PageSize = pageSize,
                Data = await query.ToListAsync()
            };

            return result;
        }

        public async Task<List<People>> GetPeopleAsync()
        {
            return await _context.Person.ToListAsync();
        }
        public async Task<List<People>> SearchPeopleAsync(string searchString)
        {
            return await _context.Person
                .Where(m => m.FirstName.Contains(searchString))
                .ToListAsync();
        }


        public async Task<People> GetPersonByIdAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task AddPersonAsync(People person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(People person)
        {
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

    }
}
