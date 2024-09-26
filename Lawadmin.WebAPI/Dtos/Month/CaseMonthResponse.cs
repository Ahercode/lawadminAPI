using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Month;

public class CaseMonthResponse
{
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    public int? CaseYearId { get; set; }
    public string? YearName { get; set; }
    public string? YearMonth { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }
}