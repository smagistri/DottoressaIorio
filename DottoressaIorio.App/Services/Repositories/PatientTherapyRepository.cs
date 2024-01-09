using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Services.Repositories;

public class PatientTherapyRepository : GenericRepository<Therapy>
{
    private readonly ApplicationDbContext context;

    public PatientTherapyRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IList<Therapy>> GetAllByPatientIdAsync(int patientId)
    {
        return await context.Therapies
            .Include(x => x.Patient)
            .Where(t => !t.Deleted && t.PatientId == patientId)
            .ToListAsync();
    }
}