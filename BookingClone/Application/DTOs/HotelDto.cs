
using TypeGen.Core.TypeAnnotations;
namespace BookingClone.Application.DTOs;

[ExportTsClass]
public class HotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
}
