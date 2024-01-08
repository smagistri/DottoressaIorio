using DottoressaIorio.App.Data;
using DottoressaIorio.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.App.Pages.Patients;

public class PatientsBase : ComponentBase
{
    protected readonly int PageSize = 10; // Number of patients per page

    private List<Patient> patients;

    [Inject] public ApplicationDbContext DbContext { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    protected List<Patient> DisplayedPatients { get; set; }
    protected List<Patient> FilteredPatients { get; set; }
    protected int CurrentPage { get; set; } = 1;
    protected int TotalPages => (int)Math.Ceiling((double)FilteredPatients.Count / PageSize);
    protected string SearchTerm { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        patients = await DbContext.Patients.ToListAsync();
        await UpdateDisplayedPatients();
    }

    protected async Task UpdateDisplayedPatients()
    {
        await FilterPatientsBySearchTermAsync();
        var startIndex = (CurrentPage - 1) * PageSize;
        DisplayedPatients = FilteredPatients.Skip(startIndex).Take(PageSize).ToList();
    }

    protected async Task FilterPatientsBySearchTermAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
        {
            FilteredPatients = patients;
        }
        else
        {
            SearchTerm = SearchTerm.ToLower();
            FilteredPatients = await DbContext.Patients
                .Where(p => !p.Deleted &&
                            (p.FirstName.ToLower().Contains(SearchTerm) ||
                             p.LastName.ToLower().Contains(SearchTerm)))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();
        }
    }

    protected async Task SearchPatients(ChangeEventArgs e)
    {
        CurrentPage = 1;
        SearchTerm = e.Value.ToString();
        await UpdateDisplayedPatients();
    }

    protected async Task HandleSelectedPage(int page)
    {
        CurrentPage = page;
        await UpdateDisplayedPatients();
    }


    protected void NavigateToEditPatient(int patientId)
    {
        NavigationManager.NavigateTo($"/patients/edit/{patientId}");
    }

    protected void NavigateToDeletePatient(int id)
    {
        NavigationManager.NavigateTo($"/patients/delete/{id}");
    }

    protected async Task PreviousPage()
    {
        if (CurrentPage != 1)
        {
            CurrentPage--;
            await UpdateDisplayedPatients();
        }
    }

    protected async Task NextPage()
    {
        if (CurrentPage != TotalPages)
        {
            CurrentPage++;
            await UpdateDisplayedPatients();
        }
    }
}