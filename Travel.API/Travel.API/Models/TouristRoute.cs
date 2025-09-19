namespace Travel.API.Models;

public class TouristRoute
{
    public Guid Id { get; set; }
    public string Title { get; set; }  // disable on '<Nullable>disable</Nullable>'
    public string Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public double? DiscountPresent { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public DateTime? DepartmentTime { get; set; }
    public string Features { get; set; }
    public string Fees { get; set; }
    public string Notes { get; set; }
    public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
}