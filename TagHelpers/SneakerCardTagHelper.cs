using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SneakerCollection.Models;
using System.Text;
using System.Text.Encodings.Web;

namespace SneakerCollection.TagHelpers
{
    /// <summary>
    /// Tag Helper personnalisé pour générer des cartes de sneakers
    /// avec un style et des fonctionnalités cohérents.
    /// Utilisation : <sneaker-card sneaker="@model" show-actions="true" />
    /// </summary>
    [HtmlTargetElement("sneaker-card")]
    public class SneakerCardTagHelper : TagHelper
    {
        /// <summary>
        /// Le modèle Sneaker à afficher dans la carte
        /// </summary>
        public Sneaker Sneaker { get; set; }

        /// <summary>
        /// Indique s’il faut afficher les boutons d’action (Voir, Modifier, Supprimer).
        /// Par défaut : true
        /// </summary>
        public bool ShowActions { get; set; } = true;

        /// <summary>
        /// Classes CSS supplémentaires à appliquer à la carte
        /// </summary>
        public string CssClass { get; set; } = "";

        /// <summary>
        /// Taille de la carte : "small", "medium", "large"
        /// Par défaut : "medium"
        /// </summary>
        public string Size { get; set; } = "medium";

        /// <summary>
        /// Indique s’il faut afficher la description complète ou tronquée.
        /// Par défaut : false (tronquée).
        /// </summary>
        public bool ShowFullDescription { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Sneaker == null)
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = "div";
            output.AddClass("card", HtmlEncoder.Default);
            output.AddClass("sneaker-card", HtmlEncoder.Default);
            output.AddClass("h-100", HtmlEncoder.Default);
            output.AddClass("shadow-sm", HtmlEncoder.Default);

            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }

            // Ajout des classes selon la taille
            switch (Size.ToLower())
            {
                case "small":
                    output.AddClass("card-small", HtmlEncoder.Default);
                    break;
                case "large":
                    output.AddClass("card-large", HtmlEncoder.Default);
                    break;
                default:
                    output.AddClass("card-medium", HtmlEncoder.Default);
                    break;
            }

            var cardContent = GenerateCardContent();
            output.Content.SetHtmlContent(cardContent);
        }

        private string GenerateCardContent()
        {
            var sb = new StringBuilder();

            // Image de la carte
            sb.Append("<div class=\"card-img-top-wrapper position-relative\">");
            if (!string.IsNullOrEmpty(Sneaker.ImageUrl))
            {
                sb.Append($"<img src=\"{Sneaker.ImageUrl}\" alt=\"{Sneaker.Brand} {Sneaker.Model}\" class=\"card-img-top sneaker-image\" style=\"height: 200px; object-fit: cover;\">");
            }
            else
            {
                sb.Append("<div class=\"card-img-placeholder d-flex align-items-center justify-content-center bg-light\" style=\"height: 200px;\">");
                sb.Append("<i class=\"fas fa-image text-muted fa-3x\"></i>");
                sb.Append("</div>");
            }

            // Badge Édition Limitée
            if (Sneaker.IsLimited)
            {
                sb.Append("<div class=\"position-absolute top-0 end-0 m-2\">");
                sb.Append("<span class=\"badge bg-warning text-dark\">");
                sb.Append("<i class=\"fas fa-gem me-1\"></i>Édition Limitée");
                sb.Append("</span>");
                sb.Append("</div>");
            }

            // Badge État
            sb.Append("<div class=\"position-absolute top-0 start-0 m-2\">");
            sb.Append(GenerateConditionBadge(Sneaker.Condition));
            sb.Append("</div>");

            sb.Append("</div>");

            // Corps de la carte
            sb.Append("<div class=\"card-body d-flex flex-column\">");

            // Marque et Modèle
            sb.Append($"<h5 class=\"card-title text-primary mb-1\">{Sneaker.Brand}</h5>");
            sb.Append($"<h6 class=\"card-subtitle mb-2 text-muted\">{Sneaker.Model}</h6>");

            // Coloris
            if (!string.IsNullOrEmpty(Sneaker.Colorway))
            {
                sb.Append($"<p class=\"text-muted small mb-2\">Couleur : {Sneaker.Colorway}</p>");
            }

            // Détails principaux
            sb.Append("<div class=\"row g-2 mb-3 small\">");
            sb.Append($"<div class=\"col-6\"><strong>Taille :</strong> US {Sneaker.Size}</div>");
            sb.Append($"<div class=\"col-6\"><strong>Catégorie :</strong> {Sneaker.Category}</div>");
            sb.Append("</div>");

            // Description
            if (!string.IsNullOrEmpty(Sneaker.Description))
            {
                var description = ShowFullDescription
                    ? Sneaker.Description
                    : (Sneaker.Description.Length > 100
                        ? Sneaker.Description.Substring(0, 100) + "..."
                        : Sneaker.Description);

                sb.Append($"<p class=\"card-text small text-muted flex-grow-1\">{description}</p>");
            }

            // Prix et Date de sortie
            sb.Append("<div class=\"mt-auto\">");
            sb.Append("<div class=\"d-flex justify-content-between align-items-center mb-2\">");
            sb.Append($"<span class=\"h5 text-success mb-0\">€{Sneaker.Price:F2}</span>");
            sb.Append($"<small class=\"text-muted\">{Sneaker.ReleaseDate:MMM yyyy}</small>");
            sb.Append("</div>");

            // Informations de stock
            if (Sneaker.StockQuantity.HasValue)
            {
                var stockClass = Sneaker.StockQuantity > 0 ? "text-success" : "text-danger";
                var stockText = Sneaker.StockQuantity > 0 ? $"{Sneaker.StockQuantity} en stock" : "Rupture de stock";
                sb.Append($"<small class=\"{stockClass}\"><i class=\"fas fa-boxes me-1\"></i>{stockText}</small>");
            }

            // Boutons d'action
            if (ShowActions)
            {
                sb.Append("<div class=\"btn-group w-100 mt-3\" role=\"group\">");
                sb.Append($"<a href=\"/Sneaker/Details/{Sneaker.Id}\" class=\"btn btn-outline-info btn-sm\">");
                sb.Append("<i class=\"fas fa-eye me-1\"></i>Voir");
                sb.Append("</a>");
                sb.Append($"<a href=\"/Sneaker/Edit/{Sneaker.Id}\" class=\"btn btn-outline-warning btn-sm\">");
                sb.Append("<i class=\"fas fa-edit me-1\"></i>Modifier");
                sb.Append("</a>");
                sb.Append($"<a href=\"/Sneaker/Delete/{Sneaker.Id}\" class=\"btn btn-outline-danger btn-sm\">");
                sb.Append("<i class=\"fas fa-trash me-1\"></i>Supprimer");
                sb.Append("</a>");
                sb.Append("</div>");
            }

            sb.Append("</div>"); // Ferme div mt-auto
            sb.Append("</div>"); // Ferme card-body

            // Pied de carte avec date d’ajout
            sb.Append("<div class=\"card-footer bg-transparent border-top-0\">");
            sb.Append($"<small class=\"text-muted\">");
            sb.Append($"<i class=\"fas fa-calendar-plus me-1\"></i>Ajoutée le {Sneaker.AddedDate:dd MMM yyyy}");
            sb.Append("</small>");
            sb.Append("</div>");

            return sb.ToString();
        }

        private string GenerateConditionBadge(SneakerCondition condition)
        {
            var (badgeClass, icon) = condition switch
            {
                SneakerCondition.DeadStock => ("bg-success", "fas fa-star"),
                SneakerCondition.NearMint => ("bg-primary", "fas fa-thumbs-up"),
                SneakerCondition.Excellent => ("bg-primary", "fas fa-check"),
                SneakerCondition.VeryGood => ("bg-secondary", "fas fa-check-circle"),
                SneakerCondition.Good => ("bg-warning", "fas fa-minus-circle"),
                SneakerCondition.Fair => ("bg-warning text-dark", "fas fa-exclamation"),
                SneakerCondition.Poor => ("bg-danger", "fas fa-times"),
                _ => ("bg-secondary", "fas fa-question")
            };

            return $"<span class=\"badge {badgeClass}\" title=\"{condition}\">" +
                   $"<i class=\"{icon} me-1\"></i>{condition}" +
                   "</span>";
        }
    }
}
