﻿using DottoressaIorio.App.Data;
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

    public async Task<IList<TherapyTemplate>> GetAllOrderedAsync()
    {
        return await context.TherapyTemplates
            .Where(x => !x.Deleted)
            .OrderBy(x => x.Title)
            .ToListAsync();
    }

    public async Task<bool> TitleExistsAsync(string title)
    {
        return await context.TherapyTemplates
            .AnyAsync(x => x.Title == title && !x.Deleted);
    }
}