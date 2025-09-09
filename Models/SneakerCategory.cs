using System.ComponentModel.DataAnnotations;

namespace SneakerCollection.Models
{
    public enum SneakerCategory
    {
        [Display(Name = "Basketball")]
        Basketball = 1,
        
        [Display(Name = "Running")]
        Running = 2,
        
        [Display(Name = "Lifestyle")]
        Lifestyle = 3,
        
        [Display(Name = "Skateboarding")]
        Skateboarding = 4,
        
        [Display(Name = "Tennis")]
        Tennis = 5,
        
        [Display(Name = "Football")]
        Football = 6,
        
        [Display(Name = "Retro")]
        Retro = 7,
        
        [Display(Name = "Limited Edition")]
        Limited = 8,
        
        [Display(Name = "Collaboration")]
        Collaboration = 9
    }
}
