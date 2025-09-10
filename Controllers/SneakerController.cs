using Microsoft.AspNetCore.Mvc;
using SneakerCollection.Models;
using SneakerCollection.Services;

namespace SneakerCollection.Controllers
{
    public class SneakerController : Controller
    {
        private readonly ISneakerService _sneakerService;

        public SneakerController(ISneakerService sneakerService)
        {
            _sneakerService = sneakerService;
        }

        // GET: Sneaker
        public IActionResult Index(string? searchTerm, SneakerBrand? brand, SneakerCategory? category, SneakerCondition? condition)
        {
            IEnumerable<Sneaker> sneakers;

            if (!string.IsNullOrEmpty(searchTerm) || brand.HasValue || category.HasValue || condition.HasValue)
            {
                sneakers = _sneakerService.SearchSneakers(searchTerm, brand, category, condition);
                ViewBag.SearchTerm = searchTerm;
                ViewBag.SelectedBrand = brand;
                ViewBag.SelectedCategory = category;
                ViewBag.SelectedCondition = condition;
            }
            else
            {
                sneakers = _sneakerService.GetAllSneakers();
            }

            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View(sneakers);
        }

        // GET: Sneaker/Details/5
        public IActionResult Details(int id)
        {
            var sneaker = _sneakerService.GetSneakerById(id);
            if (sneaker == null)
            {
                TempData["ErrorMessage"] = "Sneaker non trouvée.";
                return RedirectToAction(nameof(Index));
            }

            return View(sneaker);
        }

        // GET: Sneaker/Create
        public IActionResult Create()
        {
            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View();
        }

        // POST: Sneaker/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sneaker sneaker)
        {
            // Définir la date d'ajout
            sneaker.AddedDate = DateTime.Now;
            ModelState.Remove("AddedDate");

            if (ModelState.IsValid)
            {
                try
                {
                    _sneakerService.AddSneaker(sneaker);
                    TempData["SuccessMessage"] = $"La sneaker '{sneaker.FullName}' a été ajoutée avec succès !";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de l'ajout de la sneaker.");
                }
            }

            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View(sneaker);
        }

        // GET: Sneaker/Edit/5
        public IActionResult Edit(int id)
        {
            var sneaker = _sneakerService.GetSneakerById(id);
            if (sneaker == null)
            {
                TempData["ErrorMessage"] = "Sneaker non trouvée.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View(sneaker);
        }

        // POST: Sneaker/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Sneaker sneaker)
        {
            if (id != sneaker.Id)
            {
                TempData["ErrorMessage"] = "Identifiant de sneaker invalide.";
                return RedirectToAction(nameof(Index));
            }

            // Préserver l'AddedDate original
            var originalSneaker = _sneakerService.GetSneakerById(id);
            if (originalSneaker != null)
            {
                sneaker.AddedDate = originalSneaker.AddedDate;
            }
            ModelState.Remove("AddedDate");

            if (ModelState.IsValid)
            {
                try
                {
                    _sneakerService.UpdateSneaker(sneaker);
                    TempData["SuccessMessage"] = $"La sneaker '{sneaker.FullName}' a été modifiée avec succès !";
                    return RedirectToAction(nameof(Details), new { id = sneaker.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la modification de la sneaker.");
                }
            }

            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View(sneaker);
        }

        // GET: Sneaker/Delete/5
        public IActionResult Delete(int id)
        {
            var sneaker = _sneakerService.GetSneakerById(id);
            if (sneaker == null)
            {
                TempData["ErrorMessage"] = "Sneaker non trouvée.";
                return RedirectToAction(nameof(Index));
            }

            return View(sneaker);
        }

        // POST: Sneaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var sneaker = _sneakerService.GetSneakerById(id);
                if (sneaker != null)
                {
                    _sneakerService.DeleteSneaker(id);
                    TempData["SuccessMessage"] = $"La sneaker '{sneaker.FullName}' a été supprimée avec succès !";
                }
                else
                {
                    TempData["ErrorMessage"] = "Sneaker non trouvée.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de la sneaker.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sneaker/Search
        public IActionResult Search()
        {
            ViewBag.Brands = Enum.GetValues(typeof(SneakerBrand)).Cast<SneakerBrand>();
            ViewBag.Categories = Enum.GetValues(typeof(SneakerCategory)).Cast<SneakerCategory>();
            ViewBag.Conditions = Enum.GetValues(typeof(SneakerCondition)).Cast<SneakerCondition>();

            return View();
        }

        // POST: Sneaker/Search
        [HttpPost]
        public IActionResult Search(string? searchTerm, SneakerBrand? brand, SneakerCategory? category, SneakerCondition? condition)
        {
            return RedirectToAction(nameof(Index), new { searchTerm, brand, category, condition });
        }
    }
}