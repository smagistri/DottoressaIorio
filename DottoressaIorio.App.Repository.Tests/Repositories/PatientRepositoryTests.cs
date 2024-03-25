using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Repository.Tests.Repositories;

public class PatientRepositoryTests : IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _context;

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

    public PatientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated(); // Ensure database is created
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Theory]
    [InlineData("John", 1)]
    [InlineData(null, 2)]
    [InlineData("NonExistent", 0)]
    [InlineData("Deleted", 0)]
    [InlineData("jane", 1)]
    [InlineData("jane smith", 1)]
    [InlineData("smith jane", 1)]
    public async Task SearchPatientsAsync_ShouldReturnExpectedResults(string searchTerm, int expectedCount)
    {
        var result = await SearchPatientsAsync(searchTerm);
        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public async Task GetAllOrderedAsync_ShouldReturnAllPatientsOrdered()
    {
        var result = await GetAllOrderedAsync();
        Assert.Equal(data.Count(p => !p.Deleted), result.Count); // Excluding deleted patients
        // Assert that patients are ordered by last name then first name
        for (var i = 1; i < result.Count; i++)
        {
            Assert.True(
                string.Compare(result[i - 1].LastName, result[i].LastName, StringComparison.OrdinalIgnoreCase) <= 0);
            if (string.Equals(result[i - 1].LastName, result[i].LastName, StringComparison.OrdinalIgnoreCase))
                Assert.True(string.Compare(result[i - 1].FirstName, result[i].FirstName,
                    StringComparison.OrdinalIgnoreCase) <= 0);
        }
    }

    [Theory]
    [InlineData("John", "Doe", "10/02/1981", true)]
    [InlineData("John", "Doe", "10/02/1982", false)] // Wrong date of birth
    [InlineData("NonExistent", "Person", "01/01/2000", false)]
    [InlineData("Deleted", "Patient", "18/12/1990", false)]
    [InlineData("Jane", "Smith", "01/12/2002", true)]
    public async Task PatientExistsAsync_ShouldReturnExpectedResult(string firstName, string lastName, string dob,
        bool expectedResult)
    {
        var patient = new Patient { FirstName = firstName, LastName = lastName, DateOfBirth = DateTime.Parse(dob) };

        var result = await PatientExistsAsync(patient);

        Assert.Equal(expectedResult, result);
    }

    private async Task<IList<Patient>> SearchPatientsAsync(string searchTerm = null)
    {
        _context.Patients.AddRange(data);
        await _context.SaveChangesAsync();

        var repository = new PatientRepository(_context);
        var result = await repository.SearchPatientsAsync(searchTerm);

        return result;
    }

    private async Task<IList<Patient>> GetAllOrderedAsync()
    {
        _context.Patients.AddRange(data);
        await _context.SaveChangesAsync();

        var repository = new PatientRepository(_context);
        var result = await repository.GetAllOrderedAsync();

        return result;
    }

    private async Task<bool> PatientExistsAsync(Patient patient)
    {
        _context.Patients.AddRange(data);
        await _context.SaveChangesAsync();

        var repository = new PatientRepository(_context);
        var result = await repository.PatientExistsAsync(patient);

        return result;
    }
}