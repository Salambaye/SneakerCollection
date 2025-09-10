using SneakerCollection.Models;

namespace SneakerCollection.Services
{
    public interface ISneakerService
    {
        IEnumerable<Sneaker> GetAllSneakers();
        Sneaker? GetSneakerById(int id);
        void AddSneaker(Sneaker sneaker);
        void UpdateSneaker(Sneaker sneaker);
        void DeleteSneaker(int id);
        IEnumerable<Sneaker> SearchSneakers(string? searchTerm, SneakerBrand? brand, SneakerCategory? category, SneakerCondition? condition);
        int GetNextId();
        IEnumerable<Sneaker> GetFeaturedSneakers();
        IEnumerable<Sneaker> GetRecentSneakers(int v);
    }
}
