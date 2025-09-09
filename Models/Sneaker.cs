using System.ComponentModel.DataAnnotations;

namespace SneakerCollection.Models
{
    public class Sneaker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La marque est obligatoire")]
        [Display(Name = "Marque")]
        public SneakerBrand Brand { get; set; }

        [Required(ErrorMessage = "Le modèle est obligatoire")]
        [StringLength(100, ErrorMessage = "Le modèle ne peut pas dépasser 100 caractères")]
        [Display(Name = "Modèle")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le colorway est obligatoire")]
        [StringLength(50, ErrorMessage = "Le colorway ne peut pas dépasser 50 caractères")]
        [Display(Name = "Colorway")]
        public string Colorway { get; set; } = string.Empty;

        [Required(ErrorMessage = "La taille est obligatoire")]
        [Range(3.0, 18.0, ErrorMessage = "La taille doit être entre 3 et 18")]
        [Display(Name = "Taille (US)")]
        public decimal Size { get; set; }

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0.01, 9999.99, ErrorMessage = "Le prix doit être entre 0.01€ et 9999.99€")]
        [Display(Name = "Prix (€)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "L'état est obligatoire")]
        [Display(Name = "État")]
        public SneakerCondition Condition { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [Display(Name = "Catégorie")]
        public SneakerCategory Category { get; set; }

        [Required(ErrorMessage = "La date de sortie est obligatoire")]
        [Display(Name = "Date de sortie")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date d'ajout")]
        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "URL de l'image")]
        [Url(ErrorMessage = "L'URL de l'image n'est pas valide")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Édition limitée")]
        public bool IsLimited { get; set; }

        [Range(0, 1000, ErrorMessage = "La quantité doit être entre 0 et 1000")]
        [Display(Name = "Quantité en stock")]
        public int? StockQuantity { get; set; }

        // Propriété calculée pour l'affichage
        public string FullName => $"{Brand} {Model} \"{Colorway}\"";

        // Propriété pour l'URL par défaut si aucune image
        public string DisplayImageUrl =>
            string.IsNullOrEmpty(ImageUrl)
                ? "/images/sneaker-placeholder.jpg"
                : ImageUrl;
    }
}
