using SneakerCollection.Models;

namespace SneakerCollection.Services
{
    public class SneakerService : ISneakerService
    {
        private readonly List<Sneaker> _sneakers;

        public SneakerService()
        {
            _sneakers = GetSampleData();
        }

        public IEnumerable<Sneaker> GetAllSneakers()
        {
            return _sneakers.OrderByDescending(s => s.AddedDate);
        }

        public Sneaker? GetSneakerById(int id)
        {
            return _sneakers.FirstOrDefault(s => s.Id == id);
        }

        public void AddSneaker(Sneaker sneaker)
        {
            sneaker.Id = GetNextId();
            sneaker.AddedDate = DateTime.Now;
            _sneakers.Add(sneaker);
        }

        public void UpdateSneaker(Sneaker sneaker)
        {
            var existingSneaker = GetSneakerById(sneaker.Id);
            if (existingSneaker != null)
            {
                var index = _sneakers.IndexOf(existingSneaker);
                sneaker.AddedDate = existingSneaker.AddedDate; // Garde la date d'ajout originale
                _sneakers[index] = sneaker;
            }
        }

        public void DeleteSneaker(int id)
        {
            var sneaker = GetSneakerById(id);
            if (sneaker != null)
            {
                _sneakers.Remove(sneaker);
            }
        }

        public IEnumerable<Sneaker> SearchSneakers(string? searchTerm, SneakerBrand? brand, SneakerCategory? category, SneakerCondition? condition)
        {
            var query = _sneakers.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s =>
                    s.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.Colorway.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.Brand.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (s.Description != null && s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (brand.HasValue)
            {
                query = query.Where(s => s.Brand == brand.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(s => s.Category == category.Value);
            }

            if (condition.HasValue)
            {
                query = query.Where(s => s.Condition == condition.Value);
            }

            return query.OrderByDescending(s => s.AddedDate);
        }

        public IEnumerable<Sneaker> GetFeaturedSneakers()
        {
            return _sneakers
                .Where(s => s.IsLimited || s.Price > 200)
                .OrderByDescending(s => s.Price)
                .Take(4);
        }

        public IEnumerable<Sneaker> GetRecentSneakers(int count = 6)
        {
            return _sneakers
                .OrderByDescending(s => s.AddedDate)
                .Take(count);
        }
        public int GetNextId()
        {
            return _sneakers.Any() ? _sneakers.Max(s => s.Id) + 1 : 1;
        }

        private List<Sneaker> GetSampleData()
        {
            return new List<Sneaker>
            {
                new Sneaker
                {
                    Id = 1,
                    Brand = SneakerBrand.Jordan,
                    Model = "Air Jordan 1 Retro High",
                    Colorway = "Chicago",
                    Size = 9.0m,
                    Price = 180.00m,
                    Condition = SneakerCondition.DeadStock,
                    Category = SneakerCategory.Basketball,
                    ReleaseDate = new DateTime(2015, 5, 30),
                    AddedDate = DateTime.Now.AddDays(-30),
                    ImageUrl = "https://images.stockx.com/images/Air-Jordan-1-Retro-High-Chicago-2015.jpg",
                    Description = "Colorway emblématique de la Air Jordan 1, inspirée de l'équipe des Chicago Bulls.",
                    IsLimited = true,
                    StockQuantity = 2
                },
                new Sneaker
                {
                    Id = 2,
                    Brand = SneakerBrand.Nike,
                    Model = "Air Max 1",
                    Colorway = "Patta Waves",
                    Size = 8.5m,
                    Price = 350.00m,
                    Condition = SneakerCondition.Excellent,
                    Category = SneakerCategory.Lifestyle,
                    ReleaseDate = new DateTime(2021, 9, 18),
                    AddedDate = DateTime.Now.AddDays(-25),
                    ImageUrl = "https://www.sneakerstyle.fr/wp-content/uploads/2021/10/patta-x-nike-air-max-1-noise-aqua-DH1348-004.jpg",
                    Description = "Collaboration exclusive entre Nike et Patta avec un design inspiré des vagues.",
                    IsLimited = true,
                    StockQuantity = 1
                },
                new Sneaker
                {
                    Id = 3,
                    Brand = SneakerBrand.Adidas,
                    Model = "Yeezy Boost 350 V2",
                    Colorway = "Zebra",
                    Size = 10.0m,
                    Price = 80.00m,
                    Condition = SneakerCondition.VeryGood,
                    Category = SneakerCategory.Lifestyle,
                    ReleaseDate = new DateTime(2017, 2, 25),
                    AddedDate = DateTime.Now.AddDays(-20),
                    ImageUrl = "https://www.sneakers.fr/wp-content/uploads/2017/01/adidas-yeezy-boost-350V2-Zebra-8-380x380.jpeg",
                    Description = "L'un des colorways les plus populaires de la Yeezy 350 V2.",
                    IsLimited = true,
                    StockQuantity = 3
                },
                new Sneaker
                {
                    Id = 4,
                    Brand = SneakerBrand.Nike,
                    Model = "Dunk Low",
                    Colorway = "Panda",
                    Size = 9.5m,
                    Price = 120.00m,
                    Condition = SneakerCondition.NearMint,
                    Category = SneakerCategory.Lifestyle,
                    ReleaseDate = new DateTime(2021, 3, 10),
                    AddedDate = DateTime.Now.AddDays(-15),
                    ImageUrl = "https://images.stockx.com/images/Nike-Dunk-Low-White-Black.jpg",
                    Description = "Le colorway 'Panda' est devenu un incontournable avec son design noir et blanc classique.",
                    IsLimited = false,
                    StockQuantity = 5
                },
                new Sneaker
                {
                    Id = 5,
                    Brand = SneakerBrand.Jordan,
                    Model = "Air Jordan 4 Retro",
                    Colorway = "Black Cat",
                    Size = 8.0m,
                    Price = 220.00m,
                    Condition = SneakerCondition.DeadStock,
                    Category = SneakerCategory.Basketball,
                    ReleaseDate = new DateTime(2020, 1, 18),
                    AddedDate = DateTime.Now.AddDays(-10),
                    ImageUrl = "https://images.stockx.com/images/Air-Jordan-4-Retro-Black-Cat-2020.jpg",
                    Description = "Version entièrement noire de la mythique Air Jordan 4.",
                    IsLimited = true,
                    StockQuantity = 1
                },
                new Sneaker
                {
                    Id = 6,
                    Brand = SneakerBrand.NewBalance,
                    Model = "990v3",
                    Colorway = "Grey",
                    Size = 11.0m,
                    Price = 185.00m,
                    Condition = SneakerCondition.VeryGood,
                    Category = SneakerCategory.Running,
                    ReleaseDate = new DateTime(2012, 8, 15),
                    AddedDate = DateTime.Now.AddDays(-5),
                    Description = "Sneaker de running premium avec un confort exceptionnel.",
                    IsLimited = false,
                    StockQuantity = 2
                },
                new Sneaker
                {
                    Id = 7,
                    Brand = SneakerBrand.Vans,
                    Model = "Old Skool",
                    Colorway = "Black/White",
                    Size = 7.5m,
                    Price = 65.00m,
                    Condition = SneakerCondition.Good,
                    Category = SneakerCategory.Skateboarding,
                    ReleaseDate = new DateTime(1977, 3, 19),
                    AddedDate = DateTime.Now.AddDays(-3),
                    Description = "La sneaker de skate iconique de Vans, un classique intemporel.",
                    IsLimited = false,
                    StockQuantity = 10
                },
                new Sneaker
                {
                    Id = 8,
                    Brand = SneakerBrand.Nike,
                    Model = "Air Force 1 Low",
                    Colorway = "Triple White",
                    Size = 9.0m,
                    Price = 90.00m,
                    Condition = SneakerCondition.Excellent,
                    Category = SneakerCategory.Lifestyle,
                    ReleaseDate = new DateTime(1982, 12, 1),
                    AddedDate = DateTime.Now.AddDays(-1),
                    Description = "La sneaker la plus vendue de Nike, un incontournable en version blanche.",
                    IsLimited = false,
                    StockQuantity = 8
                }
            };
        }

       
    }
}
