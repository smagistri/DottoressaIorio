using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Services
{
    public class PatientService
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Patient> GetPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public Patient GetPatientById(int patientId)
        {
            return _dbContext.Patients.FirstOrDefault(p => p.PatientId == patientId);
        }

        public void AddPatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();
        }

        public void UpdatePatient(Patient patient)
        {
            _dbContext.Entry(patient).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletePatient(int patientId)
        {
            var patient = _dbContext.Patients.Find(patientId);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                _dbContext.SaveChanges();
            }
        }
    }
}
