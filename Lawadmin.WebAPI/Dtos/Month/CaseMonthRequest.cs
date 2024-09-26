using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Month;

public class CaseMonthRequest
{

    [StringLength(10)]
    public string? Name { get; set; }

    public int? CaseYearId { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }
}