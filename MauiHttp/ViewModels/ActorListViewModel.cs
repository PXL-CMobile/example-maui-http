using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiHttp.Models;
using MauiHttp.Services;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace MauiHttp.ViewModels;


public partial class ActorListViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Actor> _actors;

    private IHTTPService _httpService;

    public ActorListViewModel(IHTTPService service)
    {
        _httpService = service;
    }

    [RelayCommand]
    private async Task GetList()
    {
        Actors = await _httpService.GetActors();
    }

}
