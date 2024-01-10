using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Repository.Tests;

public class PatientRepositoryTests
{
    private readonly List<Patient> data = new()
    {
        new Patient
        {
            PatientId = 1, FirstName = "John", LastName = "Doe", CreatedDate = DateTime.Now,
            DateOfBirth = DateTime.Parse("10/02/1981"), Email = "s@s.com"
        },
        new Patient
        {
            PatientId = 2, FirstName = "Jane", LastName = "Smith", CreatedDate = DateTime.Now,
            DateOfBirth = DateTime.Parse("01/12/2002"), Email = "s@s1.com"
        },
        new Patient
        {
            PatientId = 3,
            FirstName = "Deleted",
            LastName = "Patient",
            Deleted = true,
            CreatedDate = DateTime.Now.AddMonths(-1),
            DateOfBirth = DateTime.Parse("18/12/1990"),
            EditDate = DateTime.Now,
            Email = "s@s2.com"
        }
    };

    private async Task<IList<Patient>> GetAllOrderedAsync(string searchTerm = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("PatientListDatabase")
            .Options;

        await using var context = new ApplicationDbContext(options);
        context.Patients.AddRange(data);
        await context.SaveChangesAsync();

        var repository = new PatientRepository(context);
        var result = await repository.GetAllOrderedAsync(searchTerm);

        await context.Database.EnsureDeletedAsync();
        return result;
    }

    [Theory]
    [InlineData("John", 1)]
    [InlineData(null, 2)]
    [InlineData("NonExistent", 0)]
    [InlineData("Deleted", 0)]
    [InlineData("jane", 1)]
    public async Task GetAllOrderedAsync_ShouldReturnExpectedResults(string searchTerm, int expectedCount)
    {
        var result = await GetAllOrderedAsync(searchTerm);
        Assert.Equal(expectedCount, result.Count);
    }
}