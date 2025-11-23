using MauiHttp.Models;

namespace MauiHttp.Services
{
    public interface IHTTPService
    {
        Task<ObservableCollection<Actor>> GetActors();
        Task<ObservableCollection<Actor>> GetFavoriteActors();
        Task AddFavoriteActor(int actorId);
        Task<bool> Login(string email, string password);
    }
}