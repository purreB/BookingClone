using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<GuestDto?> GetGuestByIdAsync(Guid id)
    {
        var guest = await userRepository.GetGuestByIdAsync(id);
        return guest == null ? null : mapper.Map<GuestDto>(guest);
    }

    public async Task<StaffUserDto?> GetStaffByIdAsync(Guid id)
    {
        var staff = await userRepository.GetStaffByIdAsync(id);
        return staff == null ? null : mapper.Map<StaffUserDto>(staff);
    }

    public async Task<GuestDto> AddGuestAsync(GuestDto guestDto)
    {
        var guest = mapper.Map<Guest>(guestDto);
        guest.Id = Guid.NewGuid();
        await userRepository.AddGuestAsync(guest);
        return mapper.Map<GuestDto>(guest);
    }

    public async Task<StaffUserDto> AddStaffAsync(StaffUserDto staffDto)
    {
        var staff = mapper.Map<StaffUser>(staffDto);
        staff.Id = Guid.NewGuid();
        await userRepository.AddStaffAsync(staff);
        return mapper.Map<StaffUserDto>(staff);
    }

    public async Task UpdateGuestAsync(GuestDto guestDto)
    {
        var guest = mapper.Map<Guest>(guestDto);
        await userRepository.UpdateGuestAsync(guest);
    }

    public async Task UpdateStaffAsync(StaffUserDto staffDto)
    {
        var staff = mapper.Map<StaffUser>(staffDto);
        await userRepository.UpdateStaffAsync(staff);
    }

    public async Task DeleteGuestAsync(Guid id)
    {
        await userRepository.DeleteGuestAsync(id);
    }

    public async Task DeleteStaffAsync(Guid id)
    {
        await userRepository.DeleteStaffAsync(id);
    }
}
