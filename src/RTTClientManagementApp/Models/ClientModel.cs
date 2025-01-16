using System.ComponentModel.DataAnnotations;

namespace RTTClientManagementApp.Models
{
    public class ClientModel
    {
 
    public int Id { get; set; }

    [Required(ErrorMessage = "Client name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; }

    public List<AddressModel> Addresses { get; set; }
      
    public List<ContactInfoModel> Contacts { get; set; }
}
}