using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Services.Repositories;

public class TherapyTemplateRepository : GenericRepository<TherapyTemplate>
{
    private readonly ApplicationDbContext context;

    public TherapyTemplateRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public override async Task<IList<TherapyTemplate>> GetAllAsync()
    {
        return await context.TherapyTemplates
            .Where(x => !x.Deleted)
            .OrderBy(x => x.Title)
            .ToListAsync();
    }

    public override async Task AddAsync(TherapyTemplate entity)
    {
        entity.CreatedDate = DateTime.Now;
        await base.AddAsync(entity);
    }

    public override async Task UpdateAsync(TherapyTemplate entity)
    {
        entity.EditDate = DateTime.Now;
        await base.UpdateAsync(entity);
    }

    public async Task DeleteAsync(TherapyTemplate entity)
    {
        entity.EditDate = DateTime.Now;
        entity.Deleted = true;
        await base.UpdateAsync(entity);
    }
}