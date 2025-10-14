using System.ComponentModel.DataAnnotations;

namespace library_management_system;

public class UpdateModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
