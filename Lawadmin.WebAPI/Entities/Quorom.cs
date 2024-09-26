using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("Quorom")]
public partial class Quorom
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("Quorom")]
    public virtual ICollection<CourtCaseQuorom> CourtCaseQuoroms { get; set; } = new List<CourtCaseQuorom>();
}
