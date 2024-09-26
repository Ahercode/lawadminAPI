using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("CaseMonth")]
public partial class CaseMonth
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    public int? CaseYearId { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? ModifiedAt { get; set; }

    [ForeignKey("CaseYearId")]
    [InverseProperty("CaseMonths")]
    public virtual CaseYear? CaseYear { get; set; }

    [InverseProperty("CaseMonth")]
    public virtual ICollection<CourtCase> CourtCases { get; set; } = new List<CourtCase>();
}
