using DottoressaIorio.App.Models;
using DottoressaIorio.App.Services.Repositories;
using Microsoft.AspNetCore.Components;

namespace DottoressaIorio.App.Pages.Patients;

public class PatientsBase : ComponentBase
{
    protected readonly int PageSize = 10; // Number of patients per page

    private IList<Patient> patients;

    [Inject] public PatientRepository Repository { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    protected IList<Patient> DisplayedPatients { get; set; }
    protected IList<Patient> FilteredPatients { get; set; }
    protected int CurrentPage { get; set; } = 1;
    protected int TotalPages => (int)Math.Ceiling((double)FilteredPatients.Count / PageSize);
    protected string SearchTerm { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        patients = await Repository.GetAllAsync();
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
            FilteredPatients = await Repository.GetFilteredPatientsAsync(SearchTerm);
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