using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel.API.Models;

public class TouristRoute
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }  // disable on '<Nullable>disable</Nullable>'
    
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }
    
    [Range(0.0, 1.0)]
    public double? DiscountPresent { get; set; }
    
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public DateTime? DepartmentTime { get; set; }
    
    [Column(TypeName = "text")]
    public string Features { get; set; }
    [Column(TypeName = "text")]
    public string Fees { get; set; }
    [Column(TypeName = "text")]
    public string Notes { get; set; }
    public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
        = new List<TouristRoutePicture>();
    
    public double? Rating { get; set; }
    public TravelDays? TravelDays { get; set; }
    public TripType? TripType { get; set; }
    public DepartureCity? DepartureCity { get; set; }
    
}