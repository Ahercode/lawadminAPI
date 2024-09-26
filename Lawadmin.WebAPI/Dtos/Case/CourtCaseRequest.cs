using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Case;

public class CourtCaseRequest
{

    public string? CaseTitle { get; set; }

    public string? CatchPhrase { get; set; }

    [StringLength(100)]
    public string? SuitNumber { get; set; }

    public string? HeadNote { get; set; }

    public string? CaseContent { get; set; }

    public string YearOfJudgement { get; set; }

    public int? CourtId { get; set; }
    public string? Quoroms { get; set; }

    public int? CaseMonthId { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }
}