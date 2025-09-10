using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace SneakerCollection.TagHelpers
{
    /// <summary>
    /// Tag Helper personnalisé pour afficher les tailles de sneakers avec conversions et formatage
    /// Utilisation : <size-tag size="9.5" system="US" show-conversions="true" />
    /// </summary>
    [HtmlTargetElement("size-tag")]
    public class SizeTagHelper : TagHelper
    {
        /// <summary>
        /// La valeur de la taille à afficher
        /// </summary>
        public decimal Size { get; set; }

        /// <summary>
        /// Système de taille : "US", "EU", "UK", "CM"
        /// Par défaut : "US"
        /// </summary>
        public string System { get; set; } = "US";

        /// <summary>
        /// Afficher les conversions vers d'autres systèmes de tailles
        /// Par défaut : false
        /// </summary>
        public bool ShowConversions { get; set; } = false;

        /// <summary>
        /// Style d'affichage : "badge", "text", "button"
        /// Par défaut : "badge"
        /// </summary>
        public string Style { get; set; } = "badge";

        /// <summary>
        /// Thème de couleur pour les badges/boutons
        /// Par défaut : "primary"
        /// </summary>
        public string Theme { get; set; } = "primary";

        /// <summary>
        /// Afficher le label du système (US, EU, etc.)
        /// Par défaut : true
        /// </summary>
        public bool ShowSystemLabel { get; set; } = true;

        /// <summary>
        /// Classes CSS supplémentaires
        /// </summary>
        public string CssClass { get; set; } = "";

        /// <summary>
        /// Afficher un indicateur de disponibilité
        /// </summary>
        public bool ShowAvailability { get; set; } = false;

        /// <summary>
        /// Niveau de stock pour l'indicateur de disponibilité (si ShowAvailability = true)
        /// </summary>
        public int? StockLevel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (Style.ToLower())
            {
                case "text":
                    GenerateTextStyle(output);
                    break;
                case "button":
                    GenerateButtonStyle(output);
                    break;
                default:
                    GenerateBadgeStyle(output);
                    break;
            }

            var content = GenerateSizeContent();
            output.Content.SetHtmlContent(content);
        }

        private void GenerateBadgeStyle(TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("badge", HtmlEncoder.Default);

            var themeClass = Theme.ToLower() switch
            {
                "secondary" => "bg-secondary",
                "success" => "bg-success",
                "warning" => "bg-warning text-dark",
                "danger" => "bg-danger",
                "info" => "bg-info",
                "light" => "bg-light text-dark",
                "dark" => "bg-dark",
                _ => "bg-primary"
            };

            output.AddClass(themeClass, HtmlEncoder.Default);

            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }
        }

        private void GenerateTextStyle(TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("size-text", HtmlEncoder.Default);

            var themeClass = Theme.ToLower() switch
            {
                "secondary" => "text-secondary",
                "success" => "text-success",
                "warning" => "text-warning",
                "danger" => "text-danger",
                "info" => "text-info",
                "muted" => "text-muted",
                _ => "text-primary"
            };

            output.AddClass(themeClass, HtmlEncoder.Default);
            output.AddClass("fw-bold", HtmlEncoder.Default);

            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }
        }

        private void GenerateButtonStyle(TagHelperOutput output)
        {
            output.TagName = "button";
            output.AddClass("btn", HtmlEncoder.Default);
            output.AddClass("btn-sm", HtmlEncoder.Default);

            var themeClass = Theme.ToLower() switch
            {
                "secondary" => "btn-secondary",
                "success" => "btn-success",
                "warning" => "btn-warning",
                "danger" => "btn-danger",
                "info" => "btn-info",
                "light" => "btn-light",
                "dark" => "btn-dark",
                _ => "btn-primary"
            };

            output.AddClass(themeClass, HtmlEncoder.Default);

            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("disabled", "disabled");

            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }
        }

        private string GenerateSizeContent()
        {
            var content = "";

            // Affichage principal de la taille
            if (ShowSystemLabel)
            {
                content += $"{System} {FormatSize(Size)}";
            }
            else
            {
                content += FormatSize(Size);
            }

            // Indicateur de disponibilité
            if (ShowAvailability && StockLevel.HasValue)
            {
                var availabilityIcon = StockLevel.Value > 0
                    ? "<i class=\"fas fa-check text-success ms-1\"></i>"
                    : "<i class=\"fas fa-times text-danger ms-1\"></i>";
                content += availabilityIcon;
            }

            // Conversions si demandées
            if (ShowConversions)
            {
                content += GenerateConversions();
            }

            return content;
        }

        private string FormatSize(decimal size)
        {
            // Supprimer les décimales inutiles
            if (size % 1 == 0)
            {
                return size.ToString("F0");
            }
            return size.ToString("F1");
        }

        private string GenerateConversions()
        {
            var conversions = "";
            var currentSystem = System.ToUpper();

            // Conversion vers les autres systèmes principaux
            if (currentSystem != "EU")
            {
                var euSize = ConvertToEU(Size, currentSystem);
                conversions += $"<br><small class=\"text-muted\">EU {FormatSize(euSize)}</small>";
            }

            if (currentSystem != "UK")
            {
                var ukSize = ConvertToUK(Size, currentSystem);
                conversions += $"<br><small class=\"text-muted\">UK {FormatSize(ukSize)}</small>";
            }

            if (currentSystem != "CM")
            {
                var cmSize = ConvertToCM(Size, currentSystem);
                conversions += $"<br><small class=\"text-muted\">{FormatSize(cmSize)} CM</small>";
            }

            return conversions;
        }

        private decimal ConvertToEU(decimal size, string fromSystem)
        {
            return fromSystem.ToUpper() switch
            {
                "US" => size + 32.5m,
                "UK" => size + 33.5m,
                "CM" => (size - 12) * 1.5m + 35,
                _ => size
            };
        }

        private decimal ConvertToUK(decimal size, string fromSystem)
        {
            return fromSystem.ToUpper() switch
            {
                "US" => size - 1,
                "EU" => size - 33.5m,
                "CM" => (size - 23) / 0.667m,
                _ => size
            };
        }

        private decimal ConvertToCM(decimal size, string fromSystem)
        {
            return fromSystem.ToUpper() switch
            {
                "US" => (size * 0.667m) + 23,
                "EU" => ((size - 35) / 1.5m) + 12,
                "UK" => (size * 0.667m) + 23.5m,
                _ => size
            };
        }
    }
}
