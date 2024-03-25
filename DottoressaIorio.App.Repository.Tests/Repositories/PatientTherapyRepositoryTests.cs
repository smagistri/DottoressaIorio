using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Repository.Tests.Repositories;

public class PatientTherapyRepositoryTests : IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly List<Therapy> _data = new()
    {
        new Therapy
        {
            TherapyId = 1,
            PatientId = 1,
            Description = "Therapy 1",
            CreatedDate = DateTime.Now,
            Deleted = false,
            Patient = new Patient
            {
                PatientId = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Parse("1981-02-10"),
                Email = "s@s.com"
            }
        }
    };

    public PatientTherapyRepositoryTests()
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
    public async Task GetAllsync_ShouldReturnTherapiesForPatient()
    {
        // Arrange
        await SeedDataAsync();

        var patientId = 1;
        var expectedTherapies = _data.Where(t => t.PatientId == patientId && !t.Deleted).OrderBy(t => t.CreatedDate)
            .ToList();

        // Act
        var repository = new PatientTherapyRepository(_context);
        var result = await repository.GetAllsync(patientId);

        // Assert
        Assert.Equal(expectedTherapies.Count, result.Count);
        Assert.Equal(expectedTherapies.Select(t => t.TherapyId), result.Select(t => t.TherapyId));
    }

    private async Task SeedDataAsync()
    {
        _context.Therapies.AddRange(_data);
        await _context.SaveChangesAsync();
    }
}