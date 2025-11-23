using MauiHttp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHttp.Services
{
    public class MOCKHTTPService : IHTTPService
    {
        private readonly List<Actor> _actors;
        private readonly List<int> _favoriteActorIds;

        public MOCKHTTPService()
        {
            _actors = GenerateMockActors();
            _favoriteActorIds = new List<int> { 2, 7 }; // Hardcoded favorites
        }

        public Task<bool> Login(string userEmail, string userPassword)
        {
            // Always succeed for mock
            return Task.FromResult(true);
        }

        public Task<bool> MakeActorFavorite(Guid id)
        {
            // Not used in mock, but could simulate adding favorites
            return Task.FromResult(true);
        }

        public Task<ObservableCollection<Actor>> GetActors()
        {
            return Task.FromResult(new ObservableCollection<Actor>(_actors));
        }

        public Task<ObservableCollection<Actor>> GetFavoriteActors()
        {
            var favorites = _actors.Where(a => _favoriteActorIds.Contains(a.Id)).ToList();
            return Task.FromResult(new ObservableCollection<Actor>(favorites));
        }

        public Task AddFavoriteActor(int actorId)
        {
            if (!_favoriteActorIds.Contains(actorId))
            {
                _favoriteActorIds.Add(actorId);
            }
            return Task.CompletedTask;
        }

        private List<Actor> GenerateMockActors()
        {
            return new List<Actor>
        {
            new Actor { Id = 1, FirstName = "Tommy", LastName = "Pickles", ProfilePictureUrl = "https://picsum.photos/100?1" },
            new Actor { Id = 2, FirstName = "Rick", LastName = "Sanchez", ProfilePictureUrl = "https://picsum.photos/100?2" },
            new Actor { Id = 3, FirstName = "Morty", LastName = "Smith", ProfilePictureUrl = "https://picsum.photos/100?3" },
            new Actor { Id = 4, FirstName = "Homer", LastName = "Simpson", ProfilePictureUrl = "https://picsum.photos/100?4" },
            new Actor { Id = 5, FirstName = "Peter", LastName = "Griffin", ProfilePictureUrl = "https://picsum.photos/100?5" },
            new Actor { Id = 6, FirstName = "Stewie", LastName = "Griffin", ProfilePictureUrl = "https://picsum.photos/100?6" },
            new Actor { Id = 7, FirstName = "Bugs", LastName = "Bunny", ProfilePictureUrl = "https://picsum.photos/100?7" },
            new Actor { Id = 8, FirstName = "Daffy", LastName = "Duck", ProfilePictureUrl = "https://picsum.photos/100?8" },
            new Actor { Id = 9, FirstName = "Scooby", LastName = "Doo", ProfilePictureUrl = "https://picsum.photos/100?9" },
            new Actor { Id = 10, FirstName = "Shaggy", LastName = "Rogers", ProfilePictureUrl = "https://picsum.photos/100?10" }
        };
        }
    }
}
