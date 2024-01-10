using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Services.Repositories;
public class PatientRepository : GenericRepository<Patient>
{
    private readonly ApplicationDbContext context;

    public PatientRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    private IQueryable<Patient> BaseQuery()
    {
        return context.Patients
            .Where(p => !p.Deleted)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName);
    }

    public async Task<IList<Patient>> GetAllOrderedAsync()
    {
        return await BaseQuery().ToListAsync();
    }

    public async Task<IList<Patient>> GetAllOrderedAsync(string searchTerm)
    {
        searchTerm = searchTerm?.ToLower();
        var query = BaseQuery();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p =>
                p.FirstName.ToLower().Contains(searchTerm) ||
                p.LastName.ToLower().Contains(searchTerm) ||
                p.Email.ToLower().Contains(searchTerm)
            );
        }

        return await query.ToListAsync();
    }
}
