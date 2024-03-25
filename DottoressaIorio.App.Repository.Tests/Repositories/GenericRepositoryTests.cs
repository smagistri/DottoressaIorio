using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Repository.Tests.Repositories;

public class GenericRepositoryTests : IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly GenericRepository<Patient> _repository;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new GenericRepository<Patient>(_context);
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
    public async Task GetByIdAsync_ShouldReturnEntityById()
    {
        // Arrange
        var entityToAdd = new Patient
        {
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "example@example.com",
            FirstName = "John",
            LastName = "Doe"
        };
        await _repository.AddAsync(entityToAdd);

        // Act
        var result = await _repository.GetByIdAsync(entityToAdd.PatientId);

        // Assert
        Assert.Equal(entityToAdd.PatientId, result.PatientId);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var entitiesToAdd = new List<Patient>
        {
            new()
            {
                DateOfBirth = DateTime.Now.AddYears(-30),
                Email = "example@example.com",
                FirstName = "John",
                LastName = "Doe"
            },
            new()
            {
                PatientId = 2,
                DateOfBirth = DateTime.Now.AddYears(-25),
                Email = "jane@example.com",
                FirstName = "Jane",
                LastName = "Smith"
            },
            new()
            {
                PatientId = 3,
                DateOfBirth = DateTime.Now.AddYears(-55),
                Email = "jane.doe@example.com",
                FirstName = "Jane",
                LastName = "Doe"
            }
        };
        foreach (var entity in entitiesToAdd) await _repository.AddAsync(entity);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(entitiesToAdd.Count, result.Count);
        Assert.Equal(entitiesToAdd.Select(e => e.PatientId), result.Select(e => e.PatientId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEntity()
    {
        // Arrange
        var entityToAdd = new Patient
        {
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "example@example.com",
            FirstName = "John",
            LastName = "Doe"
        };
        await _repository.AddAsync(entityToAdd);
        entityToAdd.FirstName = "Updated";

        // Act
        await _repository.UpdateAsync(entityToAdd);
        var result = await _repository.GetByIdAsync(entityToAdd.PatientId);

        // Assert
        Assert.Equal("Updated", result.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteEntity()
    {
        // Arrange
        var entityToAdd = new Patient
        {
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "example@example.com",
            FirstName = "John",
            LastName = "Doe"
        };
        await _repository.AddAsync(entityToAdd);

        // Act
        await _repository.DeleteAsync(entityToAdd);
        var result = await _repository.GetByIdAsync(entityToAdd.PatientId);

        // Assert
        Assert.True(result.Deleted);
    }
}