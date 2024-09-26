using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("Court")]
public partial class Court
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public string? Location { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? ModifiedAt { get; set; }

    [InverseProperty("Court")]
    public virtual ICollection<CourtCase> CourtCases { get; set; } = new List<CourtCase>();
}
