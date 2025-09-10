using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SneakerCollection.Models;
using System.Text.Encodings.Web;

namespace SneakerCollection.TagHelpers
{
    /// <summary>
    /// Tag Helper personnalisé pour afficher des badges de condition avec des couleurs et icônes cohérentes
    /// Utilisation : <condition-badge condition="@sneaker.Condition" show-icon="true" size="small" />
    /// </summary>
    [HtmlTargetElement("condition-badge")]
    public class ConditionBadgeTagHelper : TagHelper
    {
        /// <summary>
        /// La condition de la sneaker à afficher
        /// </summary>
        public SneakerCondition Condition { get; set; }

        /// <summary>
        /// Indique si une icône doit être affichée avec le texte
        /// Valeur par défaut : true
        /// </summary>
        public bool ShowIcon { get; set; } = true;

        /// <summary>
        /// Taille du badge : "small", "medium", "large"
        /// Valeur par défaut : "medium"
        /// </summary>
        public string Size { get; set; } = "medium";

        /// <summary>
        /// Indique si le nom de la condition doit être abrégé ou affiché en entier
        /// Valeur par défaut : false (nom complet)
        /// </summary>
        public bool Abbreviated { get; set; } = false;

        /// <summary>
        /// Classes CSS supplémentaires à appliquer
        /// </summary>
        public string CssClass { get; set; } = "";

        /// <summary>
        /// Indique si un tooltip avec la description de la condition doit être affiché
        /// Valeur par défaut : true
        /// </summary>
        public bool ShowTooltip { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            // Classe de base du badge
            output.AddClass("badge", HtmlEncoder.Default);

            // Style spécifique selon la condition
            var (badgeClass, icon, description) = GetConditionStyling(Condition);
            output.AddClass(badgeClass, HtmlEncoder.Default);

            // Classe spécifique selon la taille
            var sizeClass = Size.ToLower() switch
            {
                "small" => "badge-sm",
                "large" => "badge-lg",
                _ => ""
            };

            if (!string.IsNullOrEmpty(sizeClass))
            {
                output.AddClass(sizeClass, HtmlEncoder.Default);
            }

            // Ajout des classes CSS personnalisées
            if (!string.IsNullOrEmpty(CssClass))
            {
                output.AddClass(CssClass, HtmlEncoder.Default);
            }

            // Tooltip si activé
            if (ShowTooltip)
            {
                output.Attributes.SetAttribute("title", description);
                output.Attributes.SetAttribute("data-bs-toggle", "tooltip");
                output.Attributes.SetAttribute("data-bs-placement", "top");
            }

            // Génération du contenu
            var content = GenerateBadgeContent(icon);
            output.Content.SetHtmlContent(content);
        }

        private string GenerateBadgeContent(string icon)
        {
            var conditionText = Abbreviated ? GetAbbreviatedCondition(Condition) : Condition.ToString();

            if (ShowIcon)
            {
                return $"<i class=\"{icon} me-1\"></i>{conditionText}";
            }

            return conditionText;
        }

        private (string badgeClass, string icon, string description) GetConditionStyling(SneakerCondition condition)
        {
            return condition switch
            {
                SneakerCondition.DeadStock => (
                    "bg-success text-white",
                    "fas fa-star",
                    "Neuves, jamais portées - état impeccable avec emballage d'origine"
                ),
               /* SneakerCondition.VeryNearMint => (
                    "bg-info text-white",
                    "fas fa-star-half-alt",
                    "Portées une ou deux fois - très peu de traces d'usure, état proche du neuf"
                ),*/
                SneakerCondition.NearMint => (
                    "bg-primary text-white",
                    "fas fa-thumbs-up",
                    "Légères traces d'usure - très bon état général"
                ),
                SneakerCondition.Excellent => (
                    "bg-primary text-white",
                    "fas fa-check",
                    "Portées plusieurs fois - montre quelques signes d'usure mais bien entretenues"
                ),
                SneakerCondition.VeryGood => (
                    "bg-secondary text-white",
                    "fas fa-check-circle",
                    "Usure régulière - utilisation visible mais en bon état"
                ),
                SneakerCondition.Good => (
                    "bg-warning text-dark",
                    "fas fa-minus-circle",
                    "Usure importante - signes visibles mais encore portables"
                ),
                SneakerCondition.Fair => (
                    "bg-warning text-dark",
                    "fas fa-exclamation",
                    "Usure marquée - utilisation fréquente avec dommages visibles"
                ),
                SneakerCondition.Poor => (
                    "bg-danger text-white",
                    "fas fa-times",
                    "Très usées - endommagées, surtout pour pièces ou restauration"
                ),
                _ => (
                    "bg-secondary text-white",
                    "fas fa-question",
                    "Condition non spécifiée"
                )
            };
        }

        private string GetAbbreviatedCondition(SneakerCondition condition)
        {
            return condition switch
            {
                SneakerCondition.DeadStock => "DS",
                SneakerCondition.NearMint => "NM",
                SneakerCondition.Excellent => "EX",
                SneakerCondition.VeryGood => "VG",
                SneakerCondition.Good => "G",
                SneakerCondition.Fair => "F",
                SneakerCondition.Poor => "P",
                _ => "?"
            };
        }
    }
}
