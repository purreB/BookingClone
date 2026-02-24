
using TypeGen.Core.TypeAnnotations;
namespace BookingClone.Application.DTOs;

[ExportTsClass]
public class GuestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
