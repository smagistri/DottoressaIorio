﻿using DottoressaIorio.App.Data;
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

    public async Task<List<Patient>> GetFilteredPatientsAsync(string searchTerm)
    {
        return await context.Patients
            .Where(p => !p.Deleted &&
                        (p.FirstName.ToLower().Contains(searchTerm) ||
                         p.LastName.ToLower().Contains(searchTerm)))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();
    }

    public override async Task AddAsync(Patient entity)
    {
        entity.CreatedDate = DateTime.Now;
        await base.AddAsync(entity);
    }

    public override async Task UpdateAsync(Patient entity)
    {
        entity.EditDate = DateTime.Now;
        await base.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Patient entity)
    {
        entity.EditDate = DateTime.Now;
        entity.Deleted = true;
        await base.UpdateAsync(entity);
    }
}