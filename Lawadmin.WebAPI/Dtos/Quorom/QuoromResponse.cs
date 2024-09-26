using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Quorom;

public class QuoromResponse
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }
}