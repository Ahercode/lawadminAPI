using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("CourtCase")]
public partial class CourtCase
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? CaseTitle { get; set; }

    public string? CatchPhrase { get; set; }

    [StringLength(100)]
    public string? SuitNumber { get; set; }

    public string? HeadNote { get; set; }

    public string? CaseContent { get; set; }

    [StringLength(50)]
    public string? Citation { get; set; }

    public DateOnly? YearOfJudgement { get; set; }

    public int? CourtId { get; set; }

    public int? CaseMonthId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? ModifiedAt { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public string? Quoroms { get; set; }

    [ForeignKey("CaseMonthId")]
    [InverseProperty("CourtCases")]
    public virtual CaseMonth? CaseMonth { get; set; }

    [ForeignKey("CourtId")]
    [InverseProperty("CourtCases")]
    public virtual Court? Court { get; set; }

    [InverseProperty("CourtCase")]
    public virtual ICollection<CourtCaseQuorom> CourtCaseQuoroms { get; set; } = new List<CourtCaseQuorom>();
}
