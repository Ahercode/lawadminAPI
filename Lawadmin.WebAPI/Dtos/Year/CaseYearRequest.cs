using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Year;

public class CaseYearRequest
{
    
    [StringLength(10)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }
}