using System.ComponentModel.DataAnnotations;

namespace SneakerCollection.Models
{
    public enum SneakerCondition
    {
        [Display(Name = "Dead Stock (DS)")]
        DeadStock = 1,
        
        [Display(Name = "Very Near Dead Stock (VNDS)")]
        VeryNearDeadStock = 2,
        
        [Display(Name = "Near Mint")]
        NearMint = 3,
        
        [Display(Name = "Excellent")]
        Excellent = 4,
        
        [Display(Name = "Very Good")]
        VeryGood = 5,
        
        [Display(Name = "Good")]
        Good = 6,
        
        [Display(Name = "Fair")]
        Fair = 7,
        
        [Display(Name = "Poor")]
        Poor = 8
    }
}
