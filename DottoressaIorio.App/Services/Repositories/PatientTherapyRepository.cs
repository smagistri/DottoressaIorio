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

    public override async Task AddAsync(Therapy entity)
    {
        entity.CreatedDate = DateTime.Now;
        await base.AddAsync(entity);
    }

    public override async Task UpdateAsync(Therapy entity)
    {
        entity.EditDate = DateTime.Now;
        await base.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Therapy entity)
    {
        entity.EditDate = DateTime.Now;
        entity.Deleted = true;
        await base.UpdateAsync(entity);
    }
}