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
}