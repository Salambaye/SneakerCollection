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
                    ImageUrl = "https://www.bluedropshop.fr/cdn/shop/products/air-jordan-1-high-chicago-lost-and-found-2.png?v=1701245823&width=1445",
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
                    Brand = SneakerBrand.NewBalance,
                    Model = "990v3",
                    Colorway = "Grey",
                    Size = 11.0m,
                    Price = 185.00m,
                    Condition = SneakerCondition.VeryGood,
                    Category = SneakerCategory.Running,
                    ReleaseDate = new DateTime(2012, 8, 15),
                    AddedDate = DateTime.Now.AddDays(-5),
                    ImageUrl = "https://img01.ztat.net/article/spp-media-p1/91e8113ecb8b4fa292e3c7add34c2a2b/f214354f749e4122a24d2021461aef40.jpg?imwidth=1800&filter=packshot",
                    Description = "Sneaker de running premium avec un confort exceptionnel.",
                    IsLimited = false,
                    StockQuantity = 2
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
                    ImageUrl = "https://media.cdn.kaufland.de/product-images/1024x1024/f38b0c65abc3d44201a7a8fdb500a9fc.webp",
                    Description = "Le colorway 'Panda' est devenu un incontournable avec son design noir et blanc classique.",
                    IsLimited = false,
                    StockQuantity = 5
                },
                new Sneaker
                {
                    Id = 5,
                    Brand = SneakerBrand.Asics,
                    Model = "Asics Gel-NYC",
                    Colorway = "Pink Cream Pure Silver",
                    Size = 8.0m,
                    Price = 220.00m,
                    Condition = SneakerCondition.DeadStock,
                    Category = SneakerCategory.Basketball,
                    ReleaseDate = new DateTime(2020, 1, 18),
                    AddedDate = DateTime.Now.AddDays(-10),
                    //ImageUrl = "https://www.sneakerstyle.fr/wp-content/uploads/2020/01/air-jordan-4-iv-retro-black-cat-2020-CU1110-010-01.jpg",
                    ImageUrl = " https://www.globalsneakers.fr/cdn/shop/files/asics_collaboration_2048x.png?v=1727541235",
                    Description = "Version entièrement noire de la mythique Air Jordan 4.",
                    IsLimited = true,
                    StockQuantity = 1
                },
                new Sneaker
                {
                    Id = 6,
                    Brand = SneakerBrand.Adidas,
                    Model = "Campus HQ8708",
                    Colorway = "Black",
                    Size = 10.0m,
                    Price = 80.00m,
                    Condition = SneakerCondition.VeryGood,
                    Category = SneakerCategory.Lifestyle,
                    ReleaseDate = new DateTime(2017, 2, 25),
                    AddedDate = DateTime.Now.AddDays(-20),
                    ImageUrl = "https://www.sneakerstyle.fr/wp-content/uploads/2023/08/adidas-campus-00s-core-black-HQ8708-01.webp",
                    Description = "L'un des colorways les plus populaires de la Yeezy 350 V2.",
                    IsLimited = true,
                    StockQuantity = 3

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
                    ImageUrl = "https://photos6.spartoo.com/photos/485/4858117/4858117_350_A.jpg",
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
                    ImageUrl = "https://cdn.lesitedelasneaker.com/wp-content/images/2022/04/nike-air-force-1-low-07-triple-white-cw2288-111-pic02-1100x825.jpg",
                    Description = "La sneaker la plus vendue de Nike, un incontournable en version blanche.",
                    IsLimited = false,
                    StockQuantity = 8
                }
            };
        }

       
    }
}
