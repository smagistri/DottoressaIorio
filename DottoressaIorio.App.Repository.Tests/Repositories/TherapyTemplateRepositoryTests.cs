using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Repository.Tests.Repositories;

public class TherapyTemplateRepositoryTests : IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly List<TherapyTemplate> _data = new()
    {
        new TherapyTemplate
        {
            Id = 1,
            Title = "Therapy Template 1",
            Description = "Description 1",
            CreatedDate = DateTime.Now,
            Deleted = false
        },
        new TherapyTemplate
        {
            Id = 2,
            Title = "Therapy Template 2",
            Description = "Description 2",
            CreatedDate = DateTime.Now.AddMonths(-1),
            Deleted = false
        },
        new TherapyTemplate
        {
            Id = 3,
            Title = "Deleted Template",
            Description = "Deleted Description",
            CreatedDate = DateTime.Now.AddMonths(-2),
            Deleted = true
        }
    };

    public TherapyTemplateRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllOrderedAsync_ShouldReturnAllTherapyTemplatesOrdered()
    {
        // Arrange
        await SeedDataAsync();

        var expectedTemplates = _data.Where(t => !t.Deleted).OrderBy(t => t.Title).ToList();

        // Act
        var repository = new TherapyTemplateRepository(_context);
        var result = await repository.GetAllOrderedAsync();

        // Assert
        Assert.Equal(expectedTemplates.Count, result.Count);
        Assert.Equal(expectedTemplates.Select(t => t.Id), result.Select(t => t.Id));
    }

    [Theory]
    [InlineData("Therapy Template 1", true)]
    [InlineData("Therapy Template 2", true)]
    [InlineData("Deleted Template", false)]
    [InlineData("NonExistent Template", false)]
    public async Task TitleExistsAsync_ShouldReturnExpectedResult(string title, bool expectedResult)
    {
        // Arrange
        await SeedDataAsync();

        // Act
        var repository = new TherapyTemplateRepository(_context);
        var result = await repository.TitleExistsAsync(title);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    private async Task SeedDataAsync()
    {
        _context.TherapyTemplates.AddRange(_data);
        await _context.SaveChangesAsync();
    }
}