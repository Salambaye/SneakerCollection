using System.ComponentModel.DataAnnotations;

namespace SneakerCollection.Models
{
    public enum SneakerBrand
    {
        [Display(Name = "Nike")]
        Nike = 1,

        [Display(Name = "Adidas")]
        Adidas = 2,

        [Display(Name = "Jordan")]
        Jordan = 3,

        [Display(Name = "Puma")]
        Puma = 4,

        [Display(Name = "New Balance")]
        NewBalance = 5,

        [Display(Name = "Converse")]
        Converse = 6,

        [Display(Name = "Vans")]
        Vans = 7,

        [Display(Name = "Reebok")]
        Reebok = 8,

        [Display(Name = "ASICS")]
        Asics = 9,

        [Display(Name = "Other")]
        Other = 10
    }
}
