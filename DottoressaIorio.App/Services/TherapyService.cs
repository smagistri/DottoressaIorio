using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;

namespace DottoressaIorio.App.Services;

public class TherapyService
{
    private readonly ApplicationDbContext _dbContext;

    public TherapyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Therapy> GetTherapiesByPatientId(int patientId)
    {
        return _dbContext.Therapies.Where(t => t.PatientId == patientId).ToList();
    }

    public void AddTherapy(Therapy therapy)
    {
        _dbContext.Therapies.Add(therapy);
        _dbContext.SaveChanges();
    }

    // Additional CRUD operations for Therapy can be added as needed
}