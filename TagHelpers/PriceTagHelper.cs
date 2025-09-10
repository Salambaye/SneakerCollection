using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

namespace SneakerCollection.TagHelpers
{
    /// <summary>
    /// Tag Helper personnalisé pour formater et afficher les prix avec un style cohérent
    /// Utilisation : <price-tag amount="250.50" currency="EUR" show-currency-symbol="true" size="large" />
    /// </summary>
    [HtmlTargetElement("price-tag")]
    public class PriceTagHelper : TagHelper
    {
        /// <summary>
        /// Le montant du prix à afficher
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Code de la devise (EUR, USD, GBP, etc.)
        /// Valeur par défaut : "EUR"
        /// </summary>
        public string Currency { get; set; } = "EUR";

        /// <summary>
        /// Indique si le symbole monétaire doit être affiché (€, $, £)
        /// Valeur par défaut : true
        /// </summary>
        public bool ShowCurrencySymbol { get; set; } = true;

        /// <summary>
        /// Indique si le code de la devise doit être affiché (EUR, USD)
        /// Valeur par défaut : false
        /// </summary>
        public bool ShowCurrencyCode { get; set; } = false;

        /// <summary>
        /// Taille de l'affichage du prix : "small", "medium", "large", "xlarge"
        /// Valeur par défaut : "medium"
        /// </summary>
        public string Size { get; set; } = "medium";

        /// <summary>
        /// Thème de couleur : "success" (vert), "primary" (bleu), "warning" (jaune), "danger" (rouge), "muted" (gris)
        /// Valeur par défaut : "success"
        /// </summary>
        public string Theme { get; set; } = "success";

        /// <summary>
        /// Indique si le prix doit être mis en valeur avec un fond
        /// Valeur par défaut : false
        /// </summary>
        public bool Highlighted { get; set; } = false;

        /// <summary>
        /// Classes CSS supplémentaires
        /// </summary>
        public string CssClass { get; set; } = "";

        /// <summary>
        /// Indique si une comparaison avec le prix d’origine (MSRP) doit être affichée
        /// </summary>
        public decimal? OriginalPrice { get; set; }

        /// <summary>
        /// Indique si le prix doit être animé au chargement
        /// Valeur par défaut : false
        /// </summary>
        public bool Animated { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            // Ajouter les classes de base
            output.AddClass("price-tag", HtmlEncoder.Default);

            // Ajouter la classe correspondant à la taille
            var sizeClass = Size.ToLower() switch
            {
                "small" => "h6",
                "large" => "h4",
                "xlarge" => "h3",
                _ => "h5"
            };
            output.AddClass(sizeClass, HtmlEncoder.Default);

            // Ajouter la classe correspondant au thème
            var themeClass = Theme.ToLower() switch
            {
                "primary" => "text-primary",
                "warning" => "text-warning",
                "danger" => "text-danger",
                "muted" => "text-muted",
                "success" => "text-success",
                _ => "text-success"
            };
            output.AddClass(themeClass, HtmlEncoder.Default);

            // Ajouter un fond si Highlighted est activé
            if (Highlighted)
            {
                var bgClass = Theme.ToLower() switch
                {
                    "primary" => "bg-primary text-white",
                    "warning" => "bg-warning text-dark",
                    "danger" => "bg-danger text-white",
                    "success" => "bg-success text-white",
                    _ => "bg-light text-dark"
                };

                output.AddClass(bgClass, HtmlEncoder.Default);
                output.AddClass("px-2", HtmlEncoder.Default);
                output.AddClass("py-1", HtmlEncoder.Default);
                output.AddClass("rounded", HtmlEncoder.Default);
            }

            // Ajouter la classe d’animation si activé
            if (Animated)
            {
                output.AddClass("price-animated", HtmlEncoder.Default);
            }

            // Ajouter les classes CSS personnalisées
            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }

            // Ajout d'une marge inférieure pour l'espacement
            output.AddClass("mb-0", HtmlEncoder.Default);

            // Générer le contenu du prix
            var content = GeneratePriceContent();
            output.Content.SetHtmlContent(content);

            // Ajouter du CSS personnalisé pour les animations si nécessaire
            if (Animated)
            {
                output.PostElement.SetHtmlContent(
                    "<style>" +
                    ".price-animated { animation: priceSlideIn 0.5s ease-out; }" +
                    "@keyframes priceSlideIn { from { opacity: 0; transform: translateY(10px); } to { opacity: 1; transform: translateY(0); } }" +
                    "</style>"
                );
            }
        }

        private string GeneratePriceContent()
        {
            var formattedPrice = FormatPrice(Amount);
            var content = "";

            // Ajouter le symbole ou le code de la devise
            if (ShowCurrencySymbol)
            {
                var symbol = GetCurrencySymbol(Currency);
                content = $"{symbol}{formattedPrice}";
            }
            else
            {
                content = formattedPrice;
            }

            if (ShowCurrencyCode)
            {
                content += $" {Currency}";
            }

            // Ajouter la comparaison avec le prix d’origine si fourni
            if (OriginalPrice.HasValue && OriginalPrice.Value != Amount)
            {
                var originalFormatted = FormatPrice(OriginalPrice.Value);
                var symbol = ShowCurrencySymbol ? GetCurrencySymbol(Currency) : "";
                var discount = CalculateDiscountPercentage(OriginalPrice.Value, Amount);

                content += $" <small class=\"text-muted text-decoration-line-through\">{symbol}{originalFormatted}</small>";

                if (Amount < OriginalPrice.Value)
                {
                    content += $" <small class=\"badge bg-danger ms-1\">-{discount:F0}%</small>";
                }
                else if (Amount > OriginalPrice.Value)
                {
                    content += $" <small class=\"badge bg-warning text-dark ms-1\">+{Math.Abs(discount):F0}%</small>";
                }
            }

            // Ajouter des indicateurs de tendance de prix
            content += GeneratePriceTrendIndicator();

            return content;
        }

        private string FormatPrice(decimal amount)
        {
            // Formater avec le bon nombre de décimales
            if (amount % 1 == 0)
            {
                return amount.ToString("F0", CultureInfo.InvariantCulture);
            }
            return amount.ToString("F2", CultureInfo.InvariantCulture);
        }

        private string GetCurrencySymbol(string currencyCode)
        {
            return currencyCode.ToUpper() switch
            {
                "EUR" => "€",
                "USD" => "$",
                "GBP" => "£",
                "JPY" => "¥",
                "CAD" => "C$",
                "AUD" => "A$",
                "CHF" => "CHF",
                "CNY" => "¥",
                _ => currencyCode + " "
            };
        }

        private decimal CalculateDiscountPercentage(decimal original, decimal current)
        {
            if (original == 0) return 0;
            return ((original - current) / original) * 100;
        }

        private string GeneratePriceTrendIndicator()
        {
            // Ceci pourrait être amélioré pour afficher de vraies tendances de marché
            // Pour l’instant, on affiche un simple indicateur basé sur les gammes de prix
            var content = "";

            if (Amount >= 500)
            {
                content += " <i class=\"fas fa-fire text-danger ms-1\" title=\"Prix Premium\"></i>";
            }
            else if (Amount >= 200)
            {
                content += " <i class=\"fas fa-star text-warning ms-1\" title=\"Prix Moyen\"></i>";
            }
            else if (Amount <= 50)
            {
                content += " <i class=\"fas fa-tag text-success ms-1\" title=\"Prix Abordable\"></i>";
            }

            return content;
        }
    }
}
