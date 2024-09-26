using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Year;

public class CaseYearUpdate
{
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? ModifiedAt { get; set; }
}