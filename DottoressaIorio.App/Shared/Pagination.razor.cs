using Microsoft.AspNetCore.Components;

namespace DottoressaIorio.App.Shared;

public class PaginationBase : ComponentBase
{
    [Parameter] public int CurrentPage { get; set; }
    [Parameter] public int TotalPages { get; set; }
    [Parameter] public EventCallback<int> SelectedPage { get; set; }
    [Parameter] public Func<Task> PreviousPage { get; set; }
    [Parameter] public Func<Task> NextPage { get; set; }
}