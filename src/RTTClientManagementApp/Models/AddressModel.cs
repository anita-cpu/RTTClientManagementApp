
using System.ComponentModel.DataAnnotations;

namespace RTTClientManagementApp.Models
{
    public class AddressModel
    {
    public int Id { get; set; }

    [Required(ErrorMessage = "Address type is required.")]
    public string Type { get; set; }

    [Required]
    [StringLength(200)]
    public string Street { get; set; }

    public string City { get; set; }

    [StringLength(2, ErrorMessage = "State code must be 2 characters.")]
    public string State { get; set; }

    [RegularExpression(@"\d{5}", ErrorMessage = "ZIP code must be 5 digits.")]
    public string ZipCode { get; set; }
    }
}