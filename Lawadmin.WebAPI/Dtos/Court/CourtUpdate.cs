using System.ComponentModel.DataAnnotations;

namespace Lawadmin.WebAPI.Dtos.Court;

public class CourtUpdate
{
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public string? Location { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    public DateOnly? ModifiedAt { get; set; }
}