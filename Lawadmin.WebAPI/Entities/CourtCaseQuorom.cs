using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

[Table("CourtCaseQuorom")]
public partial class CourtCaseQuorom
{
    [Key]
    public int Id { get; set; }

    public int? QuoromId { get; set; }

    public int? CourtCaseId { get; set; }

    [ForeignKey("CourtCaseId")]
    [InverseProperty("CourtCaseQuoroms")]
    public virtual CourtCase? CourtCase { get; set; }

    [ForeignKey("QuoromId")]
    [InverseProperty("CourtCaseQuoroms")]
    public virtual Quorom? Quorom { get; set; }
}
