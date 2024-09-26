using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("CaseYear")]
public partial class CaseYear
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? ModifiedAt { get; set; }

    [InverseProperty("CaseYear")]
    public virtual ICollection<CaseMonth> CaseMonths { get; set; } = new List<CaseMonth>();
}
