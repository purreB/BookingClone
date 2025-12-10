namespace BookingClone.Application.DTOs;

public class StaffUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOwner { get; set; }
}
