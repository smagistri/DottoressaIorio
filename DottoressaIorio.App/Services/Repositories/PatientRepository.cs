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

    public async Task<IList<Patient>> SearchPatientsAsync(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm)) return await BaseQuery().ToListAsync();

        searchTerm = searchTerm.ToLower();
        var names = searchTerm.Split(' ');

        var query = BaseQuery();

        query = names.Aggregate(query,
            (current, name) => current.Where(p =>
                p.FirstName.ToLower().Contains(name) || p.LastName.ToLower().Contains(name) ||
                (p.FirstName.ToLower() + " " + p.LastName.ToLower()).Contains(searchTerm)));

        return await query.ToListAsync();
    }


    public async Task<bool> PatientExistsAsync(Patient patient)
    {
        var query = await GetAllOrderedAsync();
        return query.Any(p => p.FirstName.Equals(patient.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
                              p.LastName.Equals(patient.LastName, StringComparison.InvariantCultureIgnoreCase) &
                              (p.DateOfBirth == patient.DateOfBirth));
    }
}